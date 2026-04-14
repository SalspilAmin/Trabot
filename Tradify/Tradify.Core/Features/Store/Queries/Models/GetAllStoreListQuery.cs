using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Store.Queries.Models
{
    public class GetAllStoreListQuery : IRequest<List<GetAllStoresResponse>>
    {
    }
}
