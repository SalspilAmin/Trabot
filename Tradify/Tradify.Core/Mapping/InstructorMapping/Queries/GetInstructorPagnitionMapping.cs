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

        public void GetInstructorPagnitionMapping()

        {
            CreateMap<Instructors, GetInstructorPagnitionRespons>()

                  .ForMember(dest => dest.Name, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Name.ToLower())))

                  .ForMember(dest => dest.JobTitle, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.JobTitle.ToLower())))

                  .ForMember(dest => dest.AvailableToday, opt => opt.Ignore())

                  .ForMember(dest => dest.YearsOfExperience, opt => opt.MapFrom(src => src.YearsOfExperience))

               .ForMember(dest => dest.PricePerSession, opt => opt.MapFrom(src => src.PricePerSession))

                  .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice))

                  .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))



                 .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Reviews.Any() ? src.Reviews.Average(r => (double)r.Rating) : 0))

                 .ForMember(dest => dest.ReviewsCount, opt => opt.MapFrom(src => src.Reviews != null ? src.Reviews.Count : 0))

                .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.InstructorImage));



        }

    }
}

 
