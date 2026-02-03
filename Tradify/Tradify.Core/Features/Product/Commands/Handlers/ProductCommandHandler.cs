using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Product.Commands.Handlers
{
    public class ProductCommandHandler : ResponseHandler,
                                         IRequestHandler<AddProductCommand, Response<int>>

    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductService  productService;
        private readonly IMapper mapper;
        
        #endregion

        #region Constructor
        public ProductCommandHandler(IProductService productService,
                                     IMapper mapper,
                                     LocalizationService localize) : base(localize)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.localize = localize;
        }
        #endregion

        #region Methods

        // Add Product
        public async Task<Response<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Products>(request);

              var result =  await productService.AddAsync(product);
             if (result==null) return  BadRequest<int>(localize.Get("AddFailed"));
            return Success<int>(product.Id, localize.Get("ProductAddedSuccessfully"));
        }

        //// Update Product
        //public async Task<Response<int>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        //{
        //    var product = await _productRepository.GetByIdAsync(request.Id);
        //    if (product == null) return NotFound<int>();

        //    _mapper.Map(request, product); // تحديث الحقول
        //    await _productRepository.UpdateAsync(product);

        //    return Success<int>(product.Id, _localize.Get("ProductUpdatedSuccessfully"));
        //}

        //// Remove Product
        //public async Task<Response<int>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        //{
        //    var product = await _productRepository.GetByIdAsync(request.Id);
        //    if (product == null) return NotFound<int>();

        //    await _productRepository.DeleteAsync(product);

        //    return Success<int>(product.Id, _localize.Get("ProductDeletedSuccessfully"));
        //}

        #endregion
    }
}
