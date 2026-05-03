using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Certification.Queries.Results;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.CertificationMapping
{
    public partial class CertificationProfile
    {
        public void GetCertificationByInstructorMapping()
        {
            CreateMap<Certifications, GetCertificationByInstructorResponse>()

               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Title.ToLower())));
        }
    }
}
