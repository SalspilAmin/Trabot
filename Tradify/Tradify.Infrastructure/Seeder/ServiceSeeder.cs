using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Tradify.Infrastructure.Seeder
{
    public class ServiceSeeder
    {
        private static Service CreateService(int instructorId, string name)
        {
            return new Service
            {
                InstructorId = instructorId,
                Name = name

            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var services = new List<Service>()
            {

                 // Dr. Ahmed Hassan
  
                CreateService(1, "ECG"),
                CreateService(1, "Hypertension Treatment"),

               // Dr. Sara Mohamed
    
              CreateService(2, "Acne Treatment"),
              CreateService(2, "Skin Allergy Consultation"),
              CreateService(2, "Laser Hair Removal"),

               // Dr. Mostafa Ali

               CreateService(3, "Teeth Cleaning"),
               CreateService(3, "Dental Filling"),
               CreateService(3, "Teeth Whitening"),
   

              // Dr. Nada Ibrahim

               CreateService(4, "Child Checkup"),
               CreateService(4, "Vaccination"),
               CreateService(4, "Growth Monitoring"),

              // Dr. Omar Khaled

               CreateService(5, "Bone Fracture Treatment"),
               CreateService(5, "Joint Pain Consultation"),
               CreateService(5, "Sports Injury Treatment"),

              // Dr. Mariam Adel
   CreateService(6, "Pregnancy Follow-up"),
CreateService(6, "Fertility Consultation"),
CreateService(6, "Women's Health Checkup"),
    // Dr. Youssef Samir
    CreateService(7, "Migraine Treatment"),
CreateService(7, "Epilepsy Management"),
CreateService(7, "Nerve Disorder Consultation"),
    // Dr. Reem Tarek
   CreateService(8, "Anxiety Treatment"),
CreateService(8, "Depression Therapy"),
CreateService(8, "Stress Management"),

    // Dr. Hany Fathy
  CreateService(9, "Eye Examination"),
CreateService(9, "Vision Test"),
CreateService(9, "Cataract Consultation"),

    // Dr. Salma Nabil
  CreateService(10, "General Checkup"),
CreateService(10, "Fever Treatment"),
CreateService(10, "Routine Medical Consultation"),
            };


            foreach (var service in services)
            {
                var existingCertification = await context.Service.FirstOrDefaultAsync(c => c.InstructorId == service.InstructorId
                                                                                           && c.Name == service.Name);

                if (existingCertification == null)
                {
                    await context.Service.AddAsync(service);
                    await context.SaveChangesAsync();

                }
            }


        }
    }
}
