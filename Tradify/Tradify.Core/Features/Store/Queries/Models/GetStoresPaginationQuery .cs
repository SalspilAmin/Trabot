using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Store.Queries.Models
{
    public class GetStoresPaginationQuery : IRequest<PaginatedResult<GetStoresPaginationResponse>>
    {
    
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }
    }
}
