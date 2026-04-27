using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Entities.Appointments
{
    public class InstructorImage
    {
        public int Id { get; set; }
        public string MediaPath { get; set; }
        public int InstructorId { get; set; }
        public string PublicId { get; set; } // For Update At Cloudinary


        [ForeignKey(nameof(InstructorId))]
        public virtual Instructors Instructor { get; set; }
    }
}
