using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Booking.Command.Models;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.BookingMapping
{
    public partial class BookingProfile
    {
        public void AddBookingMapping() 
        {
            CreateMap<AddBookingCommand, Bookings>();
        }
    
    }
}
