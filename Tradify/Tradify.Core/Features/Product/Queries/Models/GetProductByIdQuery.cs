using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Results;

namespace Tradify.Core.Features.Product.Queries.Models
{
    public class GetProductByIdQuery :  IRequest<Response<GetProductByIdResponse>>
    {

        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }

    }
}

