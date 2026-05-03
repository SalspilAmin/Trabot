using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Entities.Appointments
{
    public class InstructorSchedules
    {
        public int Id { get; set; }

        public int InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]

        public virtual Instructors Instructor { get; set; }

        public DayOfWeek Day { get; set; }  
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int Capacity { get; set; }        // العدد الكلي ثابت)
        public int ReservedCount { get; set; } = 0; // عدد اللي حجزوا

        public int Available => Capacity - ReservedCount;
        public bool IsAvailable { get; set; } = true;

        public virtual ICollection<Bookings>? Bookings { get; set; }
    }
}
