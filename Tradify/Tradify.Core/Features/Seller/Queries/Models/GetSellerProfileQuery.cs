using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Seller.Queries.Results;

namespace Tradify.Core.Features.Seller.Queries.Models
{
    public class GetSellerProfileQuery : IRequest<Response<GetSellerByIdResponse>>
    {
    }
}
