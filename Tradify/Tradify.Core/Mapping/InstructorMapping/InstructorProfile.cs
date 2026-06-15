using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.InstructorMapping
{
    public partial class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            AddInstructorMapping();
            GetInstructorByIdMapping();
            GetInstructorPagnitionMapping();
            GetInstructorWithDiscountMapping();
            GetRecommendedInstructorsMapping();
        }
    }
   
}
