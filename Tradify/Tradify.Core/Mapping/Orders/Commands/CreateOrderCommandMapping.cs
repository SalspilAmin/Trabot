using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.Orders
{
    public partial class OrderMapping 
    {
     public void CreateOrderMapping()
        {
            CreateMap< CreateOrderModel, Tradify.Data.Entities.Orders>();
                
        }
    }
}
