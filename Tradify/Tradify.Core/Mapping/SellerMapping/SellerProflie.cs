using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.SellerMapping
{
    public partial class SellerProflie : Profile
    {
        public SellerProflie() 
        {
            AddSellerMapping();
        }
    }
}
