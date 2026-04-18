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
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;

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
            var category = mapper.Map<Categories>(request);



            var result = await cateroriesService.AddCategoriesAsync(category);//, request.SellerId);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localization.Get(result.Item1));
            }
            else
            {
                return Success<string>("Success", meta: result.Item2);
            }

        }






        public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categories = new Categories
            {
                Id = request.Id,    
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId
            };

            var result = await cateroriesService.UpdateCategory(categories);//, request.SellerId);

            if (result != "Success")
            {
                ;
                return BadRequest<string>(localization.Get(result));
            }
            else
            {
                return Success<string>(localization.Get("CategoryUpdatedSuccessfully"));
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
