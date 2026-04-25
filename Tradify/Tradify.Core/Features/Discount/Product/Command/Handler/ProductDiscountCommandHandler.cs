using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Discount.Product.Comands.Models;
using Tradify.Core.Features.Discount.Varint.Comands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Discount.Product.Comands.Handler
{
    public class ProductDiscountCommandHandler : ResponseHandler,
                                                       IRequestHandler<AddProductDiscountCommand, Response<string>>
                                                     , IRequestHandler<DeleteProductDiscountCommand, Response<string>>
    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductVariantService productVariantService;
        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly ICurrentUserService currentUserService;
        private readonly IFileService fileService;
        private readonly IProductVariantImageService productVariantImageService;
        private readonly ISellerService sellerService;


        #endregion

        #region Constructor
        public ProductDiscountCommandHandler(IProductVariantService productVariantService,
                                     IMapper mapper,
                                     IProductService productService,
                                     IStoreService storeService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize,
                                     IFileService fileService,
                                     IProductVariantImageService productVariantImageService,
                                     ISellerService sellerService) : base(localize)
        {
            this.productVariantService = productVariantService;
            this.mapper = mapper;
            this.localize = localize;
            this.storeService = storeService;
            this.productService = productService;
            this.productVariantImageService = productVariantImageService;
            this.currentUserService = currentUserService;
            this.fileService = fileService;
            this.sellerService = sellerService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductDiscountCommand request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<string>(localize.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;


            var product = await productService.GetTableNoTracking()
                .Include(p => p.Store)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId && p.Store.SellerId == seller.Id);

            if (product == null)
                return NotFound<string>(localize.Get("ProductNotFound"));

            var variants = await productVariantService.GetTableAsTracking()
               .Where(v => v.ProductId == product.Id).ToListAsync();

            foreach (var variant in variants)
            {
                variant.Discount = request.Discount;
            }

            await productVariantService.UpdateRangeAsync(variants);

            return Success<string>(localize.Get("DiscountAddedSuccessfully"));

        }




        public async Task<Response<string>> Handle(DeleteProductDiscountCommand request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<string>(localize.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;


            var product = await productService.GetTableNoTracking()
                .Include(p => p.Store)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId && p.Store.SellerId == seller.Id);

            if (product == null)
                return NotFound<string>(localize.Get("ProductNotFound"));

            var variants = await productVariantService.GetTableAsTracking()
               .Where(v => v.ProductId == product.Id).ToListAsync();

            foreach (var variant in variants)
            {
                variant.Discount = 0;
            }

            await productVariantService.UpdateRangeAsync(variants);

            return Success<string>(localize.Get("DiscountDeletedSuccessfully"));
        }


        #endregion


    }
}
