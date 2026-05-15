using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.InstructorSchedules.Queries.Results;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.InstructorSchedulesMapping
{
    public partial class InstructorSchedulesProfile
    {
        public void GetNotAvilableInstructoreSchedulesMapping()
        {
            CreateMap<InstructorSchedules, GetNotAvilableInstructoreSchedulesResponse>()
               .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToString()))

               .ForMember(dest => dest.StartTime, opt => opt.MapFrom(
                                       src => DateTime.Today.Add(src.StartTime).ToString("hh:mm tt", new CultureInfo("en-US"))))
               .ForMember(dest => dest.EndTime, opt => opt.MapFrom(
                  src => DateTime.Today.Add(src.EndTime).ToString("hh:mm tt", new CultureInfo("en-US"))))
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));
        }
    }
}

