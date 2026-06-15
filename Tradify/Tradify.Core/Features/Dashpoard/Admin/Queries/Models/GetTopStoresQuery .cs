using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Dashpoard.Admin.Queries.Models
{
    public class GetTopStoresQuery :IRequest<Response<List<TopStoresResponse>>>
    {
        public StoreType storeType { get; set; }
    }
}
