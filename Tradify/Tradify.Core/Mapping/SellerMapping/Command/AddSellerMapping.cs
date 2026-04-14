using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;

namespace Tradify.Core.Mapping.SellerMapping
{
    public partial class SellerProflie 
    {
        public void AddSellerMapping() 
        {
            CreateMap<AddSellerCommand, Sellers>();
        }
    
    }
}
