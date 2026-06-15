using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.MessageMapping
{
    public partial class MessageProfile : Profile
    {
        public MessageProfile() 
        {
            MessageMapping();
        }
    }
}
