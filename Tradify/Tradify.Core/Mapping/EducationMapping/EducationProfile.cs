using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.EducationMapping
{
    public partial class EducationProfile : Profile
    {
        public EducationProfile()
        {
            AddEducationMapping();
            GetEducationByInstructorMapping();
        }
    }
    
}
