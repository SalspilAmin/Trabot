using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.InstructorService.Command.Models;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.InstructorServiceMapping
{
    public partial class InstructorServiceProfile
    {
        public void AddInstructorServiceMapping() 
        {
            CreateMap<AddInstructorServiceCommand, Data.Entities.Appointments.Service>();

        }

    }
}
