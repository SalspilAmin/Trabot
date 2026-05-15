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



    // =========================
    // Body Gym
    // =========================

    // Captain Ahmed Samy
    CreateCertification(14, "NASM Certified Personal Trainer"),
    CreateCertification(14, "Strength & Conditioning Specialist"),
    CreateCertification(14, "Sports Nutrition Certification"),

    // Captain Nour Ali
    CreateCertification(15, "ACE Personal Trainer Certification"),
    CreateCertification(15, "Functional Training Specialist"),

    // Captain Mostafa Adel
    CreateCertification(16, "Professional Bodybuilding Coach"),
    CreateCertification(16, "Advanced Resistance Training Certification"),

    // Captain Omar Khaled
    CreateCertification(17, "CrossFit Level 2 Trainer"),
    CreateCertification(17, "HIIT Training Specialist"),

    // Captain Youssef Nabil
    CreateCertification(18, "Certified Nutrition Coach"),
    CreateCertification(18, "Weight Management Specialist"),


    // =========================
    // Smart Learn Center
    // =========================

    // Mr. Ahmed Hassan
    CreateCertification(19, "STEM Education Certification"),
    CreateCertification(19, "Advanced Mathematics Teaching Methods"),

    // Ms. Sara Mohamed
    CreateCertification(20, "TESOL Certification"),
    CreateCertification(20, "IELTS Preparation Instructor"),

    // Mr. Mostafa Ali
    CreateCertification(21, "Physics Laboratory Safety Training"),
    CreateCertification(21, "Modern Teaching Strategies"),

    // Ms. Nada Ibrahim
    CreateCertification(22, "Biology Lab Management Certification"),

    // Mr. Omar Khaled
    CreateCertification(23, "Organic Chemistry Lab Training"),
    CreateCertification(23, "Scientific Research Methods"),

    // Ms. Mariam Adel
    CreateCertification(24, "Full Stack Web Development"),
    CreateCertification(24, "Python Programming Instructor"),

    // Mr. Youssef Samir
    CreateCertification(25, "Arabic Grammar Teaching Certification"),

    // Ms. Reem Tarek
    CreateCertification(26, "DALF Certification"),
    CreateCertification(26, "French Conversation Instructor"),

    // Mr. Hany Fathy
    CreateCertification(27, "Modern History Teaching Skills"),

    // Ms. Salma Nabil
    CreateCertification(28, "SAT Preparation Instructor"),
    CreateCertification(28, "Critical Thinking Skills Training"),


    // =========================
    // Flash Studio
    // =========================

    // Ahmed Samy
    CreateCertification(29, "Professional Photography Certification"),
    CreateCertification(29, "Portrait Photography Specialist"),

    // Sara Adel
    CreateCertification(30, "Adobe Photoshop Expert"),
    CreateCertification(30, "Advanced Photo Retouching"),

    // Mostafa Nabil
    CreateCertification(31, "Cinematic Videography Certification"),
    CreateCertification(31, "Video Editing with Premiere Pro"),

    // Reem Tarek
    CreateCertification(32, "Creative Direction Certification"),
    CreateCertification(32, "Visual Storytelling Workshop"),


    // =========================
    // Smile Care Clinic
    // =========================

    // Dr. Kareem El-Sayed
    CreateCertification(33, "Advanced Dental Implantology"),
    CreateCertification(33, "Oral Surgery Specialist"),
    CreateCertification(33, "Digital Smile Design Certification"),

    // Dr. Laila Farouk
    CreateCertification(34, "Certified Orthodontic Specialist"),
    CreateCertification(34, "Invisalign Provider Certification"),

    // Dr. Hossam Abdelrahman
    CreateCertification(35, "General Dentistry License"),
    CreateCertification(35, "Teeth Whitening Specialist"),

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
