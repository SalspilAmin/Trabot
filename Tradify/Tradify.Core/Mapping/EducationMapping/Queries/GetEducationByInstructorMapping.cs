using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.EducationMapping
{
    public partial class EducationProfile
    {
        public void GetEducationByInstructorMapping()
        {
            CreateMap<Education, GetEducationByInstructorResponse>()
              
               .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Institution.ToLower())))
               .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree))
               .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));
        }
    }
}
