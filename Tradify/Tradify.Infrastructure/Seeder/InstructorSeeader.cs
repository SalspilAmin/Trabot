using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.Context;

namespace Tradify.Infrastructure.Seeder
{
    public static class InstructorSeeader
    {
        private static Instructors CreateInstructor(
        int id,
        int userId,
        int storeId,
        string name,
        string jobTitle,
        string about,
        decimal pricePerSession,
        int yearsOfExperience,
        bool isActive)
        {
            return new Instructors
            {
                Id = id,
                UserId = userId,
                StoreId = storeId,
                Name = name,
                JobTitle = jobTitle,
                About = about,
                PricePerSession = pricePerSession,
                YearsOfExperience = yearsOfExperience,
                Discount = 0,
                IsActive = isActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if ( context.Instructors.Any())
                return;

            var instructors = new List<Instructors>
        {
            CreateInstructor(1, 1, 1, "Dr. Ahmed Hassan", "Cardiologist",
                "Heart specialist with long experience.", 500, 12, true),

            CreateInstructor(2, 2, 1, "Dr. Sara Mohamed", "Dermatologist",
                "Skin and cosmetic treatments specialist.", 400, 8, true),

            CreateInstructor(3, 3, 1, "Dr. Mostafa Ali", "Dentist",
                "Dental care and oral surgery expert.", 300, 10, true),

            CreateInstructor(4, 4, 2, "Dr. Nada Ibrahim", "Pediatrician",
                "Child health specialist.", 350, 9, true),

            CreateInstructor(5, 5, 2, "Dr. Omar Khaled", "Orthopedic",
                "Bone and joint specialist.", 450, 11, true),

            CreateInstructor(6, 6, 2, "Dr. Mariam Adel", "Gynecologist",
                "Women health specialist.", 500, 10, true),

            CreateInstructor(7, 7, 3, "Dr. Youssef Samir", "Neurologist",
                "Brain and nerve specialist.", 600, 13, true),

            CreateInstructor(8, 8, 3, "Dr. Reem Tarek", "Psychiatrist",
                "Mental health specialist.", 420, 7, true),

            CreateInstructor(9, 9, 3, "Dr. Hany Fathy", "Ophthalmologist",
                "Eye care specialist.", 380, 9, true),

            CreateInstructor(10, 10, 3, "Dr. Salma Nabil", "General Practitioner",
                "General medicine doctor.", 250, 5, true)
        };

            // Fast existence check (better than looping)
            var existingIds =  context.Instructors
                .Select(x => x.Id)
                .ToList();

            var newInstructors = instructors
                .Where(x => !existingIds.Contains(x.Id))
                .ToList();

            if (newInstructors.Any())
            {
                await context.Instructors.AddRangeAsync(newInstructors);
                await context.SaveChangesAsync();
            }
        }

    }
}
