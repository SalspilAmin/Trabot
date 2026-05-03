using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.InstructorMapping
{
    public partial class InstructorProfile 
    {
        public void AddInstructorMapping()
        {
            CreateMap<AddInstructorCommand, Instructors>();
            CreateMap<AddInstructorWithImageCommand, Instructors>();
        }
    
    }
}
