using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Core.Features.Categorie.Queries.Models;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Categorie.Commands.Handlers
{
    public class CategoriesCommandHandler : ResponseHandler, IRequestHandler<AddCategoryCommand, Response<string>>
                                                           , IRequestHandler<UpdateCategoryCommand, Response<string>>
                                                           , IRequestHandler<DeleteCategoryCommand, Response<string>>
                                                           , IRequestHandler<RestoreCategoryCommand, Response<string>>
        






    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly ICurrentUserService currentUserService;
        private readonly ICateroriesService cateroriesService;
        private readonly IProductService productService;        




        #endregion

        #region Constructor

        public CategoriesCommandHandler(LocalizationService localization,
             IMapper mapper, ICurrentUserService currentUserService
            , ICateroriesService cateroriesService, IProductService productService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.currentUserService = currentUserService;
            this.cateroriesService = cateroriesService;
            this.productService = productService;       

        }

        #endregion

        #region Mehtods
        public async Task<Response<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentCategoryId.HasValue)
            {
                var parent = await cateroriesService.GetTableAsTracking()
                    .FirstOrDefaultAsync(c=>c.Id == request.ParentCategoryId.Value);
                if (parent == null)
                    return BadRequest<string>(localization.Get("ParentCategoryNotFound"));
            }

             

            // Check duplicate name (optional but professional)
            var exists = await cateroriesService
                .GetTableNoTracking()
                .AnyAsync(c => c.Name == request.Name 
                && c.ParentCategoryId == request.ParentCategoryId);

            if (exists)
                return BadRequest<string>(localization.Get("CategoryNameAlreadyExists"));

            var category = mapper.Map<Categories>(request);
            
            await cateroriesService.AddAsync(category);
            await cateroriesService.SaveChangesAsync();
            return Success(localization.Get("CategoryAddSuccessfully"));
        }


        public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await cateroriesService
                .GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (category == null)
                return NotFound<string>(localization.Get("CategoryNotFound"));

            // Check Parent exists
            if (request.ParentCategoryId.HasValue)
            {
                var parent = await cateroriesService
                    .GetTableNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == request.ParentCategoryId.Value);

                if (parent == null)
                    return BadRequest<string>(localization.Get("ParentCategoryNotFound"));

                


                // ❗ منع Circular
                if (await IsCircular(request.Id, request.ParentCategoryId.Value))
                    return BadRequest<string>(localization.Get("InvalidParentCircularReference"));
            }

            // Check duplicate
            var exists = await cateroriesService
                .GetTableNoTracking()
                .AnyAsync(c =>
                    c.Id != request.Id &&
                    EF.Functions.Like(c.Name, request.Name) &&
                    c.ParentCategoryId == request.ParentCategoryId);

            if (exists)
                return BadRequest<string>(localization.Get("CategoryNameAlreadyExists"));

            // Update
            
            category.Name = request.Name;
            category.ParentCategoryId = request.ParentCategoryId;

            await cateroriesService.SaveChangesAsync();

            return Success(localization.Get("CategoryUpdatedSuccessfully"));
        }

        // 🔥 recursion check
        private async Task<bool> IsCircular(int categoryId, int newParentId)
        {
            var current = newParentId;

            while (true)
            {
                var parent = await cateroriesService
                    .GetTableNoTracking()
                    .Where(c => c.Id == current)
                    .Select(c => c.ParentCategoryId)
                    .FirstOrDefaultAsync();

                if (parent == null)
                    return false;

                if (parent == categoryId)
                    return true;

                current = parent.Value;
            }
        }


        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {

            var category = await cateroriesService
                .GetTableAsTracking()
                .Include(c=>c.Products)
                .FirstOrDefaultAsync(v =>
                    v.Id == request.Id ,
                    cancellationToken);

            if (category == null)
                return NotFound<string>(localization.Get("CategoryNotFound"));

            if (category.IsDeleted)
                return BadRequest<string>(localization.Get("CategoryAlreadyDeleted"));

            var hasChildren = await cateroriesService
                       .GetTableNoTracking()
                       .AnyAsync(c => c.ParentCategoryId == category.Id && !c.IsDeleted);

            if (hasChildren)
                return BadRequest<string>(localization.Get("CategoryHasChildren"));

            var hasProducts = await productService.GetTableNoTracking()
                .AnyAsync(p => p.CategoryId == category.Id && !p.IsDeleted);

            if (hasProducts)
                return BadRequest<string>(localization.Get("CategoryHasProducts"));


            // Soft Delete
            category.IsDeleted = true;
            await cateroriesService.UpdateAsync(category);

            
            await cateroriesService.SaveChangesAsync();

            return Success<string>(localization.Get("CategoryDeletedSuccessfully"));
        }

        public async Task<Response<string>> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {

            var category = await cateroriesService
                .GetTableAsTracking()
               
                .FirstOrDefaultAsync(v =>
                    v.Id == request.Id,
                 
                    cancellationToken);

            if (category == null)
                return NotFound<string>(localization.Get("CategoryNotFound"));

         

            if (!category.IsDeleted)
                return BadRequest<string>(localization.Get("CategoryIsNotDeleted"));


            // Restore Variant
            category.IsDeleted = false;
            await cateroriesService.UpdateAsync(category);
            await cateroriesService.SaveChangesAsync();

            return Success<string>(localization.Get("CategoryRestoredSuccessfully"));
        }
        #endregion
    }
}
