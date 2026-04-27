using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Entities.Appointments
{
    public class Service
    {
        // Services , Specializations
        public int Id { get; set; }

        public int InstructorId { get; set; }

        public string Name { get; set; }
        // HIIT, Strength Training

        [ForeignKey(nameof(InstructorId))]

        public virtual Instructors Instructor { get; set; }
    }
}
