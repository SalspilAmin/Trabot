using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.InstructorService.Queries.Results;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.InstructorServiceMapping
{
    public partial class InstructorServiceProfile
    {
        public void GetInstructorServiceMapping()
        {
            CreateMap<Data.Entities.Appointments.Service, GetInstructorServiceResponse>()

               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Name.ToLower())));
              
        }
    }
}
