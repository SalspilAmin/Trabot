using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace Tradify.Infrastructure.Seeder
{
    public class CertificationsSeeder
    {
        private static Certifications CreateCertification(int instructorId, string title)
        {
            return new Certifications
            {
                InstructorId = instructorId,
                Title = title
               
            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var certifications = new List<Certifications>()
            {

                 // Dr. Ahmed Hassan
  
                CreateCertification(1, "Advanced Cardiac Life Support (ACLS)"),
                CreateCertification(1, "Echocardiography Certification"),

               // Dr. Sara Mohamed
    
               CreateCertification(2, "Certified Dermatologist"),
               CreateCertification(2, "Laser Treatment Certification"),
               CreateCertification(2, "Cosmetic Dermatology Diploma"),

               // Dr. Mostafa Ali

               CreateCertification(3, "Certified Dental Surgeon"),
               CreateCertification(3, "Teeth Whitening Specialist"),
               CreateCertification(3, "Restorative Dentistry Certificate"),
   

              // Dr. Nada Ibrahim

    CreateCertification(4, "Pediatric Advanced Life Support (PALS)"),
CreateCertification(4, "Child Nutrition Certification"),
CreateCertification(4, "Vaccination Management Training"),

    // Dr. Omar Khaled
    CreateCertification(5, "Board Certified Orthopedic Surgeon"),
CreateCertification(5, "Sports Injury Treatment Certificate"),
CreateCertification(5, "Joint Replacement Surgery Training"),

    // Dr. Mariam Adel
    CreateCertification(6, "Obstetrics & Gynecology Specialist"),
CreateCertification(6, "Fertility Treatment Certification"),
CreateCertification(6, "Ultrasound Diagnostic Training"),
    // Dr. Youssef Samir
    CreateCertification(7, "Board Certified Neurologist"),
CreateCertification(7, "Stroke Management Certification"),
CreateCertification(7, "EEG Interpretation Training"),
    // Dr. Reem Tarek
    CreateCertification(8, "Licensed Psychiatrist"),
CreateCertification(8, "Cognitive Behavioral Therapy (CBT)"),
CreateCertification(8, "Mental Health Crisis Management"),

    // Dr. Hany Fathy
   CreateCertification(9, "Board Certified Ophthalmologist"),
CreateCertification(9, "Cataract Surgery Training"),
CreateCertification(9, "Vision Correction Specialist"),

    // Dr. Salma Nabil
   CreateCertification(10, "General Medicine Practitioner License"),
CreateCertification(10, "Primary Care Certification"),
CreateCertification(10, "Emergency First Aid Training"),
            };


            foreach (var certification in certifications)
            {
                var existingCertification = await context.Certifications.FirstOrDefaultAsync(c => c.InstructorId == certification.InstructorId
                                                                                           && c.Title== certification.Title);

                if (existingCertification == null)
                {
                    await context.Certifications.AddAsync(certification);
                    await context.SaveChangesAsync();

                }
            }


        }
    }
}
