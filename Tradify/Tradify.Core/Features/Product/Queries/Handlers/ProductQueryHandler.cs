using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Product.Queries.Handlers
{
    public class ProductQueryHandler : ResponseHandler , IRequestHandler<GetProductPaginationQuery, PaginatedResult<GetProductPaginationReponse>>
                                                       , IRequestHandler<GetProductByIdQuery, Response<GetProductByIdResponse>>
    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IProductService productService;
        #endregion

        #region Constructor
        public ProductQueryHandler(LocalizationService localization,IMapper mapper , IProductService productService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.productService= productService;
        }

        #endregion

        #region Mehtods
       public async Task<PaginatedResult<GetProductPaginationReponse>> Handle(GetProductPaginationQuery request,CancellationToken cancellationToken)
        {
            var products = productService.GetTableNoTracking()
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages);

            var result = await mapper
                .ProjectTo<GetProductPaginationReponse>(products)
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
        }

        public async Task<Response<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var Product = await productService.GetByIdWithIncludesAsync(request.Id);
            if (Product == null) return NotFound<GetProductByIdResponse>(localization.Get("NotFound"));

            var result = mapper.Map<GetProductByIdResponse>(Product);

            return Success<GetProductByIdResponse>(result);

        }
        #endregion
    }
}
