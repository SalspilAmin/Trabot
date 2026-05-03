using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.InstructorSchedulesMapping
{
    public partial class InstructorSchedulesProfile : Profile
    {
        public InstructorSchedulesProfile()
        {
            AddInstructorSchedulesMapping();
            GetInstructorSchedulesMapping();
        }
    }
    
}
