using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Booking.Queries.Results;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.BookingMapping
{
    public partial class BookingProfile
    {
        public void GetInstructorBookingMapping()
        {
            CreateMap<Bookings, GetInstructorBookingResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                 .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Schedule.Day.ToString()))


                 .ForMember(dest => dest.StartTime, opt => opt.MapFrom(
                                       src => DateTime.Today.Add(src.Schedule.StartTime).ToString("hh:mm tt", new CultureInfo("en-US"))))

                 .ForMember(dest => dest.EndTime, opt => opt.MapFrom(
                  src => DateTime.Today.Add(src.Schedule.EndTime).ToString("hh:mm tt", new CultureInfo("en-US"))))


                 .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.Status.ToString()))

                 .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate.ToString("yyyy-MM-dd")))

                 .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.PhoneNumber))


                 .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Customer.UserName.ToLower())));




        }

    }
}

