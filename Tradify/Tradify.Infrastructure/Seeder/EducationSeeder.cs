using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.Context;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Infrastructure.Seeder
{
    public class EducationSeeder
    {
        private static Education CreateEducation(int instructorId, string degree, string institution, int year)
        {
            return new Education
            {
                InstructorId = instructorId,
                Degree = degree,
                Institution = institution,
                Year = year
            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var educations = new List<Education>()
            {
                 // Dr. Ahmed Hassan
                CreateEducation(1, "MSc Cardiology", "Ain Shams University", 2014),
                CreateEducation(1, "Fellowship-Interventional Cardiology", "European Society of Cardiology", 2017),

                // Dr. Sara Mohamed
                CreateEducation(2, "MBBS ", "Alexandria University", 2013),
                CreateEducation(2, "MSc Dermatology", "Cairo University", 2016),
                CreateEducation(2, "Dip. Aesthetic Med", "American Academy of Aesthetic Medicine", 2018),

                // Dr. Mostafa Ali
                CreateEducation(3, "BDS", "Mansoura University", 2012),
                CreateEducation(3, "PhD Restorative Dentistry", "Cairo University", 2015),
                CreateEducation(3, "MSc Prosthodontics", "Cairo University",  2015 ),

                // Dr. Nada Ibrahim
                CreateEducation(4, "MBBS", "Mansoura University", 2014),
                CreateEducation(4, "IBCLC", "Cairo University", 2017),

                // Dr. Omar Khaled
                CreateEducation(5, "MBBS", "Ain Shams University", 2008),
                CreateEducation(5, "MSc Orthopedic Surgery", "Cairo University", 2012),
                CreateEducation(5, "Fellowship-Sports Medicine", "Royal College of Surgeons", 2016),

                // Dr. Mariam Adel
                CreateEducation(6, "MBBS", "Alexandria University", 2011),
                CreateEducation(6, "MSc Obstetrics & Gynecology", "Ain Shams University", 2015),

                // Dr. Youssef Samir
                CreateEducation(7, "MBBS", "Cairo University", 2009),
                CreateEducation(7, "Doctorate in Neurology", "Ain Shams University", 2015),

                // Dr. Reem Tarek
                CreateEducation(8, "MBBS", "Helwan University", 2015),
                CreateEducation(8, "MSc Psychiatry", "Cairo University", 2019),

                // Dr. Hany Fathy
                CreateEducation(9, "MBBS", "Zagazig University", 2015),
                CreateEducation(9, "MSc  Ophthalmology", "Ain Shams University", 2018),

                // Dr. Salma Nabil
                CreateEducation(10, "MBBS", "Benha University", 2010),
                CreateEducation(10, "Dip. Family Medicine", "Cairo University", 2018),
                CreateEducation(10, "Cert. Primary Healthcare", "WHO", 2020),

            };


            foreach (var education in educations)
            {
                var existingEducation = await context.Education.FirstOrDefaultAsync(e=>e.InstructorId== education.InstructorId
                                                                                     &&e.Institution==education.Institution
                                                                                     &&e.Year==education.Year);

                if (existingEducation == null)
                {
                    await context.Education.AddAsync(education);
                    await context.SaveChangesAsync();

                }
            }


        }
    }
}
