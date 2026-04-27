using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Data.Entities.Appointments
{
    public class Instructors
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string About { get; set; }

        public int YearsOfExperience { get; set; }

        public decimal PricePerSession { get; set; }


        public bool IsActive { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(StoreId))]

        public virtual Stores? Store { get; set; }

        public virtual ICollection<Reviews>? Reviews { get; set; }
        public virtual InstructorImage? InstructorImage { get; set; }
        public virtual ICollection<Education>? Educations { get; set; }
        public virtual ICollection<Certifications>? Certifications { get; set; }
        public virtual ICollection<Service>? Services { get; set; }

         public virtual ICollection<InstructorSchedules>? Schedules { get; set; }
    }
}
