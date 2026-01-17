using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Data.Entities
{
    public  class Appointment
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

      
        public int? CustomerId { get; set; }
          
        [ForeignKey(nameof(CustomerId))]
        public User? Customer { get; set; }


        
        public int? InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public Sellers? Instructor { get; set; }



        

        public int? StoreBookingId { get; set; }
        
        [ForeignKey(nameof(StoreBookingId))]
        public StoreBooking? StoreBooking { get; set; }

    }
}
