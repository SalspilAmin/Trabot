using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.InstructorSchedules.Command.Models;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.InstructorSchedulesMapping
{
    public partial class InstructorSchedulesProfile
    {
        public void AddInstructorSchedulesMapping() 
        {
            CreateMap<AddInstructorSchedulesCommand, InstructorSchedules>()
                .ForMember(dest => dest.StartTime,
                          opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))

                .ForMember(dest => dest.EndTime,
                                 opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)))
                ;

        }

    }
}
