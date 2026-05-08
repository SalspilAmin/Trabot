using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Tradify.Core.Features.Booking.Queries.Results;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.BookingMapping
{
    public partial class BookingProfile 
    {
        public void GetUserBookingMapping()
        {
            CreateMap<Bookings, GetUserBookingResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                 .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Schedule.Day.ToString()))


                 .ForMember(dest => dest.StartTime, opt => opt.MapFrom(
                                       src => DateTime.Today.Add(src.Schedule.StartTime).ToString("hh:mm tt", new CultureInfo("en-US"))))

                 .ForMember(dest => dest.EndTime, opt => opt.MapFrom(
                  src => DateTime.Today.Add(src.Schedule.EndTime).ToString("hh:mm tt", new CultureInfo("en-US"))))


                 .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.Status.ToString()))

                 .ForMember(dest => dest.BookingDate,opt => opt.MapFrom(src => src.BookingDate.ToString("yyyy-MM-dd")))

                 .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Store.Name.ToLower())))


                 .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Instructor.Name.ToLower())));




        }
    
    }
}
