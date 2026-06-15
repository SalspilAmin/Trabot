using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Mapping.StoreMapping
{
    public partial class StoreProfile : Profile
    {

        public StoreProfile()
        {
            AddStoreMapping();
            GetStoreByIdMapping();
            GetStoresPaginationMapping();
            TopStoresMapping();
        }
    }
}
