using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Seller.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Seller.Queries.Models
{
    public class GetSellerNotHaveStoreQuery : IRequest<PaginatedResult<GetAllSellerResponse>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }


        public bool? IsActive { get; set; }

    }
}

