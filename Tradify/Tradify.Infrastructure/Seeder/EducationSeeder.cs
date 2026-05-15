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

                   // =========================
// Body Gym
// =========================

CreateEducation(14, "Bachelor of Physical Education", "Helwan University", 2016),
CreateEducation(14, "Diploma in Sports Nutrition", "Cairo University", 2018),

CreateEducation(15, "Bachelor of Physical Education", "Alexandria University", 2019),

CreateEducation(16, "BSc Sports Science", "Cairo University", 2015),
CreateEducation(16, "MSc Sports Training", "Ain Shams University", 2018),


CreateEducation(18, "BSc Nutrition Science", "Mansoura University", 2017),


// =========================
// Smart Learn Center
// =========================

CreateEducation(19, "BSc Mathematics", "Cairo University", 2012),
CreateEducation(19, "Diploma in Education", "Ain Shams University", 2014),
CreateEducation(19, "MSc Applied Mathematics", "Cairo University", 2017),

CreateEducation(20, "BA English Literature", "Alexandria University", 2013),
CreateEducation(20, "Diploma in Teaching English", "British University in Egypt", 2016),

CreateEducation(21, "BSc Physics", "Mansoura University", 2011),
CreateEducation(21, "MSc Applied Physics", "Cairo University", 2015),

CreateEducation(22, "BSc Biology", "Zagazig University", 2014),

CreateEducation(23, "BSc Chemistry", "Ain Shams University", 2010),
CreateEducation(23, "MSc Organic Chemistry", "Cairo University", 2014),

CreateEducation(24, "BSc Computer Science", "Helwan University", 2015),
CreateEducation(24, "Diploma in Software Engineering", "Cairo University", 2017),

CreateEducation(25, "BA Arabic Language", "Cairo University", 2009),
CreateEducation(25, "Diploma in Arabic Literature", "Ain Shams University", 2012),

CreateEducation(26, "BA French Language", "Ain Shams University", 2013),

CreateEducation(27, "BA History", "Alexandria University", 2008),
CreateEducation(27, "Diploma in Modern History", "Cairo University", 2011),

CreateEducation(28, "BSc Engineering", "Benha University", 2012),
CreateEducation(28, "MBA", "Arab Academy for Science and Technology", 2018),


// =========================
// Flash Studio
// =========================

CreateEducation(29, "Bachelor of Fine Arts", "Helwan University", 2014),

CreateEducation(30, "Diploma in Graphic Design", "Modern Academy", 2016),

CreateEducation(31, "Bachelor of Media Production", "6th October University", 2015),

CreateEducation(32, "Bachelor of Media Arts", "6th October University", 2012),
CreateEducation(32, "Master in Creative Media", "Cairo University", 2017),


// =========================
// Smile Care Clinic
// =========================

CreateEducation(33, "BDS", "Cairo University", 2009),
CreateEducation(33, "MSc Oral Surgery", "Ain Shams University", 2014),
CreateEducation(33, "PhD Dental Surgery", "Cairo University", 2019),

CreateEducation(34, "BDS", "Alexandria University", 2011),
CreateEducation(34, "MSc Orthodontics", "Cairo University", 2016),

CreateEducation(35, "BDS", "Mansoura University", 2013),
CreateEducation(35, "Diploma in Cosmetic Dentistry", "Ain Shams University", 2017),


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
