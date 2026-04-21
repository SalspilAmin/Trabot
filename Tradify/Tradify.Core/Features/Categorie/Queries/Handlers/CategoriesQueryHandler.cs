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
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Categorie.Queries.Handlers
{
    public class CategoriesQueryHandler : ResponseHandler, IRequestHandler<GetAllCategoriesQuery, PaginatedResult<GetAllCategoriesResponse>>
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
        public async Task<PaginatedResult<GetAllCategoriesResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = cateroriesService
                        .GetTableNoTracking();
            // Store
            if (request.StoreId.HasValue)
            {
                query = query.Where(s =>
                    s.StoreId == request.StoreId);
            }

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
            var categories = query.OrderByDescending(p => p.Id);


            var result = await mapper.ProjectTo<GetAllCategoriesResponse>(categories)
                                     .ToPaginationlist(request.PageNumber, request.PageSize);
            return result;
        }


        public async Task<Response<List<GetCategoryTreeResponse>>> Handle(GetCategoryTreeQuery request, CancellationToken cancellationToken)
        {
            var categories = await cateroriesService
                .GetTableNoTracking()
                .ToListAsync(cancellationToken);
            var lookup = categories.ToLookup(c => c.ParentCategoryId);

            foreach (var item in categories)
            {
                Console.WriteLine($"Id: {item.Id}, Parent: {item.ParentCategoryId}");
            }

            var rootCategories = lookup[null];

            

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



