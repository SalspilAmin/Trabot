using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Data.Entities
{
    public class StoreBooking
    {
        public int Id { get; set; }

        
        public int? StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores? Store { get; set; }

        
        public int InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public User? Instructor { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
