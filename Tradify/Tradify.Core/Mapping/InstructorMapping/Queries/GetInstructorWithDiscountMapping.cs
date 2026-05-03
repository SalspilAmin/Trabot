using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.InstructorMapping
{
    public partial class InstructorProfile
    {

        public void GetInstructorWithDiscountMapping()

        {
            CreateMap<Instructors, GetInstructorWithDiscountResponse>()

                  .ForMember(dest => dest.Name, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Name.ToLower())))

                  .ForMember(dest => dest.JobTitle, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.JobTitle.ToLower())))

                  .ForMember(dest => dest.PricePerSession, opt => opt.MapFrom(src => src.PricePerSession))

                  .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice))

                  .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))

                .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.InstructorImage));



        }

    }
}
