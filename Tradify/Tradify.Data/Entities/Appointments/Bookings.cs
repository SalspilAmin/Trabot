using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities.Appointments
{
    public class Bookings
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public BookingStatus Status { get; set; }

        public int? CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual User Customer { get; set; }

        public int ScheduleId { get; set; }

        [ForeignKey(nameof(ScheduleId))]
        public virtual InstructorSchedules Schedule { get; set; }

        public int? InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        public virtual Instructors Instructor { get; set; }


        public int StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public virtual Stores Store { get; set; }

    }
}
