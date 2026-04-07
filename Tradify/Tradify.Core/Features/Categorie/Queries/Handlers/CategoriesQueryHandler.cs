using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Queries.Models;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Core.Features.Favorites.Queries.Models;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Categorie.Queries.Handlers
{
    public class CategoriesQueryHandler : ResponseHandler, IRequestHandler<GetAllCategoriesQuery, Response<List<GetAllCategoriesResponse>>>
                                                          , IRequestHandler<GetCategoryTreeQuery, Response<List<GetCategoryTreeResponse>>>
                                                          , IRequestHandler<GetCategoryByIdQuery, Response<GetCategoryByIdResponse>>



    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly ICurrentUserService currentUserService;
        private readonly ICateroriesService cateroriesService;




        #endregion

        #region Constructor

        public CategoriesQueryHandler(LocalizationService localization,
             IMapper mapper, ICurrentUserService currentUserService
            , ICateroriesService cateroriesService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.currentUserService = currentUserService;
            this.cateroriesService = cateroriesService;

        }

        #endregion

        #region Mehtods
        public async Task<Response<List<GetAllCategoriesResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = cateroriesService
                        .GetTableNoTracking();
            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(p =>
                              EF.Functions.Like(p.Name, $"%{search}%"));
            }
            if (request.IsDeleted.HasValue)
            {
                query = query.IgnoreQueryFilters()
                                   .Where( c=> c.IsDeleted == request.IsDeleted);
            }
            //Parent
            if (request.OnlyRoot)
            {
                query = query.Where(c => c.ParentCategoryId == null);
            }
            //childrean
            if (request.ParentCategoryId.HasValue)
            {
                query = query.Where(c => c.ParentCategoryId == request.ParentCategoryId);
            }

            var categories = await query.ToListAsync();

            var result = mapper.Map<List<GetAllCategoriesResponse>>(categories);
            return Success(result);
        }


        public async Task<Response<List<GetCategoryTreeResponse>>> Handle(GetCategoryTreeQuery request, CancellationToken cancellationToken)
        {
            var categories = await cateroriesService
                .GetTableNoTracking()
                .ToListAsync(cancellationToken);
            var lookup = categories.ToLookup(c => c.ParentCategoryId);



            // Build tree: select root categories
            var rootCategories = lookup[null];

            //var rootCategories = categories
            //    .Where(c => c.ParentCategoryId == null)
            //    .ToList();

            var result = rootCategories.Select(c => BuildTree(c, lookup)).ToList();

            return Success(result);

        }

        private GetCategoryTreeResponse BuildTree(Categories category, ILookup<int?, Categories> lookup)
        {
            return new GetCategoryTreeResponse
            {
                Id = category.Id,
                Name = category.Name,

                Children = lookup[category.Id]
                    //.Where(c => c.ParentCategoryId == category.Id)
                    .Select(child => BuildTree(child, lookup))
                    .ToList()
            };
        }



        public async Task<Response<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var query = cateroriesService
                .GetTableNoTracking()
                .Where(c => c.Id == request.Id);

            // Parent
            query = query.Include(c => c.Parent);

            // Children 
            if (request.IncludeChildren)
            {
                query = query.Include(c => c.Children);
            }

            var category = await query.FirstOrDefaultAsync(cancellationToken);

            if (category == null)
                return NotFound<GetCategoryByIdResponse>(localization.Get("CategoryNotFound"));

            var result = mapper.Map<GetCategoryByIdResponse>(category);

            // Handle Parent
            if (category.Parent != null)
            {
                result.Parent = mapper.Map<ParentCategoryDto>(category.Parent);


            }

                // Handle children 
                if (request.IncludeChildren && category.Children != null)
                {
                    result.Children = category.Children
                        .Select(c => mapper.Map<ChildCategoryDto>(c))
                        .ToList();
                }

                return Success(result);
            }
        #endregion
        }
    }



