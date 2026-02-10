using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Product.Commands.Handlers
{
    public class ProductCommandHandler : ResponseHandler,
                                         IRequestHandler<AddProductCommand, Response<int>> ,
                                         IRequestHandler<UpdateProductCommand, Response<string>>,
                                         IRequestHandler<DeleteProductCommand, Response<int>>



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
             if (result==null) return  BadRequest<int>(localize.Get("FailedToAddProduct"));
            await productService.SaveChangesAsync();
            return Success<int>(product.Id, localize.Get("ProductAddedSuccessfully"));
        }

     
        //// Update Product
        public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productService.GetByIdAsync(request.Id);
            if (product == null) return NotFound<string>(localize.Get("ProductNotFound"));

            mapper.Map(request, product); 
            await productService.UpdateAsync(product);
            await productService.SaveChangesAsync();

            return Success<string>( localize.Get("ProductUpdatedSuccessfully"));
        }

        //// Remove Product
        
        public async Task<Response<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productService.GetByIdAsync(request.Id);

            if (product == null)
                return NotFound<int>(localize.Get("ProductNotFound"));

             await productService.DeleteAsync(product);
            await productService.SaveChangesAsync();

            return Success<int>(request.Id, localize.Get("ProductDeletedSuccessfully"));
        }
        #endregion
    }
}
