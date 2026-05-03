using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Discount.Varint.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Discount.Varint.Command.Handler
{
    public class ProductVarintDiscountCommandHandler : ResponseHandler,
                                                       IRequestHandler<AddDiscountCommand, Response<string>>
                                                     , IRequestHandler<DeleteDiscountCommand, Response<string>>
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
        public ProductVarintDiscountCommandHandler(IProductVariantService productVariantService,
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
        public async Task<Response<string>> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<string>(localize.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;


            var variant = await productVariantService.GetTableAsTracking()
                .Include(v => v.Product)
                .ThenInclude(p => p.Store)
                .FirstOrDefaultAsync(v => v.Id == request.VariantId && v.Product.Store.SellerId == seller.Id);

            if (variant == null)
                return NotFound<string>(localize.Get("ProductVariantNotFound"));


            variant.Discount = request.Discount;

            await productVariantService.UpdateAsync(variant);
            await productVariantService.SaveChangesAsync();

            return Success<string>(localize.Get("DiscountAddedSuccessfully"));


        }




        public async Task<Response<string>> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<string>(localize.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;


            var variant = await productVariantService.GetTableAsTracking()
                .Include(v => v.Product)
                .ThenInclude(p => p.Store)
                .FirstOrDefaultAsync(v => v.Id == request.VariantId && v.Product.Store.SellerId == seller.Id);

            if (variant == null)
                return NotFound<string>(localize.Get("ProductVariantNotFound"));

            variant.Discount = 0;

            await productVariantService.UpdateAsync(variant);
            await productVariantService.SaveChangesAsync();

            return Success<string>(localize.Get("DiscountDeletedSuccessfully"));
        }


        #endregion


    }
}
