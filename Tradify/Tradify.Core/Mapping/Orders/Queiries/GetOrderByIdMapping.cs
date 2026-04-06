using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Core.Features.Order.Queries.Results;

namespace Tradify.Core.Mapping.Orders
{
    public partial class OrderMapping
    {

        public void GetOrderByIdMapping()
        {
            CreateMap<Tradify.Data.Entities.Orders,OrderResultQueiry>();

        }


    }
}
