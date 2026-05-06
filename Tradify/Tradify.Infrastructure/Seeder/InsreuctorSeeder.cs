using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Tradify.Infrastructure.Seeder
{
    public class InsreuctorSeeder
    {
        private static Instructors AddInstructor(int userId, int storeId, string name, string jobTitle, string about, decimal price, int yearsOfExperience, decimal discount)
        {
            return new Instructors
            {
                UserId = userId,
                StoreId = storeId,
                Name = name,
                JobTitle = jobTitle,
                About = about,
                PricePerSession = price,
                YearsOfExperience = yearsOfExperience,
                Discount = discount,
                IsActive = true
            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {

            var instructors = new List<Instructors>()
        {
        AddInstructor(14, 8, "Dr. Ahmed Hassan", "Cardiologist",
            "Consultant Cardiologist with extensive experience in diagnosing and treating heart diseases, hypertension, and cardiovascular conditions. Dedicated to providing personalized treatment plans, preventive care, and lifestyle guidance to improve patients’ overall heart health and quality of life.",
            300, 10, 0),

        AddInstructor(15, 8, "Dr. Sara Mohamed", "Dermatologist",
            "Experienced Dermatologist specializing in skin disorders, acne treatment, cosmetic dermatology, and laser procedures. Passionate about helping patients achieve healthy skin through advanced treatments, accurate diagnosis, and customized skincare solutions.",
            250, 7, 50),

        AddInstructor(16, 8, "Dr. Mostafa Ali", "Dentist",
            "Professional Dentist focused on restorative dentistry, cosmetic procedures, teeth whitening, and preventive oral healthcare. Committed to delivering high-quality dental care using modern techniques to ensure patient comfort and long-lasting results.",
            200, 8, 0),

        AddInstructor(17, 8, "Dr. Nada Ibrahim", "Pediatrician",
            "Dedicated Pediatrician providing comprehensive healthcare for infants, children, and adolescents. Experienced in managing childhood illnesses, growth monitoring, vaccinations, and offering guidance to parents for healthy child development.",
            220, 6, 0),

        AddInstructor(18, 8, "Dr. Omar Khaled", "Orthopedic Surgeon",
            "Orthopedic Surgeon specialized in treating bone, joint, and sports-related injuries with both surgical and non-surgical approaches. Focused on helping patients restore mobility, reduce pain, and return to their daily activities safely and effectively.",
            350, 12, 0),

        AddInstructor(19, 8, "Dr. Mariam Adel", "Gynecologist",
            "Experienced Gynecologist and Obstetrician providing comprehensive women’s healthcare, pregnancy follow-up, fertility consultations, and preventive screenings. Dedicated to supporting women through every stage of life with compassionate and professional care.",
            300, 9, 20),

        AddInstructor(20, 8, "Dr. Youssef Samir", "Neurologist",
            "Consultant Neurologist with expertise in diagnosing and managing neurological disorders including migraines, epilepsy, stroke, and nerve-related conditions. Passionate about improving patients’ neurological health through accurate diagnosis and advanced treatment plans.",
            320, 11, 0),

        AddInstructor(21, 8, "Dr. Reem Tarek", "Psychiatrist",
            "Licensed Psychiatrist experienced in treating anxiety, depression, stress disorders, and other mental health conditions. Committed to creating a supportive environment where patients feel comfortable discussing their concerns and receiving evidence-based care.",
            280, 5, 30),

        AddInstructor(22, 8, "Dr. Hany Fathy", "Ophthalmologist",
            "Skilled Ophthalmologist specializing in vision correction, cataract management, eye examinations, and treatment of various eye diseases. Focused on preserving and improving patients’ vision through modern diagnostic and treatment techniques.",
            260, 8, 0),

        AddInstructor(23, 8, "Dr. Salma Nabil", "General Practitioner",
            "General Practitioner providing primary healthcare services, routine medical checkups, diagnosis, and treatment for a wide range of health conditions. Dedicated to delivering patient-centered care and promoting long-term wellness and disease prevention.",
            150, 8, 0)
        };



            foreach (var instructor in instructors)
            {
                var existinginstructor = await context.Instructors.FirstOrDefaultAsync(s => s.Name == instructor.Name);

                if (existinginstructor == null)
                {
                    await context.Instructors.AddAsync(instructor);
                    await context.SaveChangesAsync();

                }
            }

        }
    }
}
