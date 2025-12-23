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

        [ForeignKey(nameof(CustomerId))]
        public int CustomerId { get; set; }
        public User? Customer { get; set; }


        [ForeignKey(nameof(InstructorId))]
        public int InstructorId { get; set; }
        public User? Instructor { get; set; }



        [ForeignKey(nameof(StoreBookingId))]

        public int StoreBookingId { get; set; }
        public StoreBooking? StoreBooking { get; set; }

    }
}
