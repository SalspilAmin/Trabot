using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.InstructorServiceMapping
{
    public partial class InstructorServiceProfile : Profile
    {
        public InstructorServiceProfile()
        {
            AddInstructorServiceMapping();
            GetInstructorServiceMapping();
        }
    }
    
}
