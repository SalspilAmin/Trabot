using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.Interactions
{
    public partial  class InteractionProfile : Profile
    {
        public InteractionProfile() {
            AddInteraction();
        }
    }
}
