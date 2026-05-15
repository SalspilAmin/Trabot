using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Infrastructure.Seeder
{
    public static class InstructorSeeader
    {
        private static Instructors CreateInstructor(
        int userId,
        int storeId,
        string name,
        string jobTitle,
        string about,
        decimal pricePerSession,
        int yearsOfExperience
       )
        {
            return new Instructors
            {
                UserId = userId,
                StoreId = storeId,
                Name = name,
                JobTitle = jobTitle,
                About = about,
                PricePerSession = pricePerSession,
                YearsOfExperience = yearsOfExperience,
                Discount = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {

            var instructors = new List<Instructors>
        {
             //   Instructor List  For Life Care Clinic

             CreateInstructor(14, 8, "Dr. Ahmed Hassan", "Cardiologist",
            "Consultant Cardiologist with extensive experience in diagnosing and treating heart diseases, hypertension, and cardiovascular conditions. Dedicated to providing personalized treatment plans, preventive care, and lifestyle guidance to improve patients’ overall heart health and quality of life.",
            300, 10),

        CreateInstructor(15, 8, "Dr. Sara Mohamed", "Dermatologist",
            "Experienced Dermatologist specializing in skin disorders, acne treatment, cosmetic dermatology, and laser procedures. Passionate about helping patients achieve healthy skin through advanced treatments, accurate diagnosis, and customized skincare solutions.",
            250, 7),

        CreateInstructor(16, 8, "Dr. Mostafa Ali", "Dentist",
            "Professional Dentist focused on restorative dentistry, cosmetic procedures, teeth whitening, and preventive oral healthcare. Committed to delivering high-quality dental care using modern techniques to ensure patient comfort and long-lasting results.",
            200, 8),

        CreateInstructor(17, 8, "Dr. Nada Ibrahim", "Pediatrician",
            "Dedicated Pediatrician providing comprehensive healthcare for infants, children, and adolescents. Experienced in managing childhood illnesses, growth monitoring, vaccinations, and offering guidance to parents for healthy child development.",
            220, 6),

        CreateInstructor(18, 8, "Dr. Omar Khaled", "Orthopedic Surgeon",
            "Orthopedic Surgeon specialized in treating bone, joint, and sports-related injuries with both surgical and non-surgical approaches. Focused on helping patients restore mobility, reduce pain, and return to their daily activities safely and effectively.",
            350, 12),

        CreateInstructor(19, 8, "Dr. Mariam Adel", "Gynecologist",
            "Experienced Gynecologist and Obstetrician providing comprehensive women’s healthcare, pregnancy follow-up, fertility consultations, and preventive screenings. Dedicated to supporting women through every stage of life with compassionate and professional care.",
            300, 9),

        CreateInstructor(20, 8, "Dr. Youssef Samir", "Neurologist",
            "Consultant Neurologist with expertise in diagnosing and managing neurological disorders including migraines, epilepsy, stroke, and nerve-related conditions. Passionate about improving patients’ neurological health through accurate diagnosis and advanced treatment plans.",
            320, 11),

        CreateInstructor(21, 8, "Dr. Reem Tarek", "Psychiatrist",
            "Licensed Psychiatrist experienced in treating anxiety, depression, stress disorders, and other mental health conditions. Committed to creating a supportive environment where patients feel comfortable discussing their concerns and receiving evidence-based care.",
            280, 5),

        CreateInstructor(22, 8, "Dr. Hany Fathy", "Ophthalmologist",
            "Skilled Ophthalmologist specializing in vision correction, cataract management, eye examinations, and treatment of various eye diseases. Focused on preserving and improving patients’ vision through modern diagnostic and treatment techniques.",
            260, 8),

        CreateInstructor(23, 8, "Dr. Salma Nabil", "General Practitioner",
            "General Practitioner providing primary healthcare services, routine medical checkups, diagnosis, and treatment for a wide range of health conditions. Dedicated to delivering patient-centered care and promoting long-term wellness and disease prevention.",
            150, 8),

            //Instructor List  For Body Gym

                CreateInstructor( 41, 9, "Captain Ahmed Samy", "Fitness Coach",
                    "Certified fitness coach specialized in strength training, muscle building, and body transformation programs. Passionate about helping clients achieve sustainable fitness results through personalized workout plans and healthy lifestyle guidance."
                    , 120, 10),

                CreateInstructor(42, 9, "Captain Nour Ali", "Personal Trainer",
                    "Experienced personal trainer focused on weight loss, endurance improvement, and functional training. Dedicated to motivating members and creating customized fitness routines suitable for all fitness levels."
                    , 80, 8),

                CreateInstructor(43, 9, "Captain Mostafa Adel", "Bodybuilding Coach",
                    "Professional bodybuilding coach with extensive experience in muscle gain, competition preparation, and advanced resistance training techniques. Committed to maximizing performance and physical strength safely."
                    , 200, 7),

                CreateInstructor(44, 9, "Captain Omar Khaled", "CrossFit Trainer",
                    "CrossFit trainer experienced in high-intensity interval training, strength conditioning, and athletic performance improvement. Passionate about building stamina, agility, and overall body fitness."
                    , 300, 9),

                CreateInstructor(45, 9, "Captain Youssef Nabil", "Nutrition Coach",
                    "Certified nutrition and fitness coach helping clients combine proper nutrition with effective training programs to achieve healthy weight management and long-term fitness goals."
                    , 100, 5),


                //Instructor List  For Smart Learn Center

                 CreateInstructor(56, 10, "Mr. Ahmed Hassan", "Mathematics Instructor",
                    "Experienced mathematics instructor specialized in algebra, geometry, calculus, and problem-solving techniques. Dedicated to simplifying complex concepts and helping students achieve academic excellence.",
                    180, 8),

                CreateInstructor(57, 10, "Ms. Sara Mohamed", "English Language Teacher",
                    "Professional English teacher focused on grammar, conversation, writing, and IELTS preparation. Passionate about improving students’ communication skills through interactive learning methods.",
                    170, 6),

                CreateInstructor(58, 10, "Mr. Mostafa Ali", "Physics Instructor",
                    "Physics instructor experienced in teaching mechanics, electricity, and modern physics concepts using practical examples and engaging explanations to enhance students’ understanding.",
                    200, 7),

                CreateInstructor(59, 10, "Ms. Nada Ibrahim", "Biology Teacher",
                    "Dedicated biology teacher helping students understand human biology, genetics, and environmental sciences through simplified explanations and practical learning approaches.",
                    160, 5),

                CreateInstructor(60, 10, "Mr. Omar Khaled", "Chemistry Instructor",
                    "Chemistry instructor specialized in organic and inorganic chemistry with a focus on laboratory skills, scientific thinking, and exam preparation strategies.",
                    210, 9),

                CreateInstructor(61, 10, "Ms. Mariam Adel", "Computer Science Instructor",
                    "Computer science instructor teaching programming fundamentals, problem solving, and software development concepts using modern technologies and hands-on projects.",
                    250, 6),

                CreateInstructor(62, 10, "Mr. Youssef Samir", "Arabic Language Teacher",
                    "Arabic language teacher experienced in grammar, literature, and writing skills development. Passionate about improving students’ reading comprehension and language fluency.",
                    150, 8),

                CreateInstructor(63, 10, "Ms. Reem Tarek", "French Language Instructor",
                    "Certified French language instructor helping students develop speaking, listening, and writing skills through engaging and interactive educational techniques.",
                    190, 4),

                CreateInstructor(64, 10, "Mr. Hany Fathy", "History Teacher",
                    "History teacher specialized in simplifying historical events and connecting them with modern contexts to make learning more engaging and memorable for students.",
                    140, 7),

                CreateInstructor(65, 10, "Ms. Salma Nabil", "SAT Preparation Instructor",
                    "SAT preparation instructor focused on critical thinking, problem-solving strategies, and exam techniques to help students achieve high scores and academic success.",
                    300, 10),

               //Instructor List  For  Flash Studio

                CreateInstructor(66, 11, "Ahmed Samy", "Professional Photographer",
                    "Professional photographer specialized in portrait, wedding, and event photography. Passionate about capturing memorable moments with creativity and high-quality editing techniques.",
                    350, 8),

                CreateInstructor(67, 11, "Sara Adel", "Photo Editor",
                    "Creative photo editor experienced in retouching, color correction, and advanced image enhancement techniques to deliver visually stunning and professional results.",
                    250, 5),

                CreateInstructor(68, 11, "Mostafa Nabil", "Videographer",
                    "Experienced videographer specialized in cinematic video production, event coverage, and promotional content creation using modern filming equipment and editing tools.",
                    400, 7),

                CreateInstructor(69, 11, "Reem Tarek", "Creative Director",
                    "Creative director focused on visual storytelling, photography concepts, and studio production management to ensure unique and professional media experiences.",
                    500, 10),


               //Instructor List  For  Smile Care Clinic

                 CreateInstructor(70, 12, "Dr. Kareem El-Sayed", "Dental Surgeon",
                       "Experienced dental surgeon specialized in advanced oral surgeries, dental implants, and complex restorative procedures using modern techniques.",
                        400, 10),

                 CreateInstructor(71, 12, "Dr. Laila Farouk", "Orthodontist",
                        "Orthodontist focused on teeth alignment, braces, and clear aligner systems to help patients achieve a confident and healthy smile.",
                         350, 7),

                 CreateInstructor(72, 12, "Dr. Hossam Abdelrahman", "General Dentist",
                        "General dentist providing comprehensive oral care including fillings, cleaning, whitening, and preventive dental treatments for all ages.",
                          250, 6)
            };






            foreach (var instructor in instructors)
            {
                var existingCertification = await context.Instructors.FirstOrDefaultAsync(c => c.UserId == instructor.UserId
                &&c.Name==instructor.Name);

                if (existingCertification == null)
                {
                    await context.Instructors.AddAsync(instructor);
                    await context.SaveChangesAsync();

                }
            }
        }

    }
}
