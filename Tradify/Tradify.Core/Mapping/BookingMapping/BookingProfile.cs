using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.BookingMapping
{
   public partial class BookingProfile : Profile
   {
        public BookingProfile() 
        {
            AddBookingMapping();
            GetUserBookingMapping();
            GetInstructorBookingMapping();
        }
   }
}
