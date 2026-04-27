using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Entities.Appointments
{
    public class Education
    {
        public int Id { get; set; }

        public int InstructorId { get; set; }

        public string Degree { get; set; }
        public string Institution { get; set; } // Name of School
        public int Year { get; set; }

        [ForeignKey(nameof(InstructorId))]

        public virtual Instructors Instructor { get; set; }

    }
}
