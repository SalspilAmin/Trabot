using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Tradify.Infrastructure.Seeder
{
    public class InstructorSchedulesSeeder
    {

        private static InstructorSchedules AddSchedule(int instructorId, DayOfWeek day, TimeSpan start, TimeSpan end, int capacity)
        {
            return new InstructorSchedules
            {
                InstructorId = instructorId,
                Day = day,
                StartTime = start,
                EndTime = end,
                Capacity = capacity,
                IsAvailable = true

            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var instructorSchedules = new List<InstructorSchedules>()
            {

        // Dr. Ahmed Hassan (Cardiologist) - InstructorId = 1

        AddSchedule(1, DayOfWeek.Sunday,   TimeSpan.FromHours(9), TimeSpan.FromHours(14), 20),
        AddSchedule(1, DayOfWeek.Monday,   TimeSpan.FromHours(17), TimeSpan.FromHours(20), 6),
        AddSchedule(1, DayOfWeek.Wednesday,TimeSpan.FromHours(16),TimeSpan.FromHours(20), 10),
        AddSchedule(1, DayOfWeek.Thursday, TimeSpan.FromHours(10),TimeSpan.FromHours(14), 15),

        // Dr. Sara Mohamed (Dermatologist) - InstructorId = 2

        AddSchedule(2, DayOfWeek.Sunday,   TimeSpan.FromHours(10), TimeSpan.FromHours(14), 6),
        AddSchedule(2, DayOfWeek.Tuesday,  TimeSpan.FromHours(10), TimeSpan.FromHours(14), 6),
        AddSchedule(2, DayOfWeek.Thursday, TimeSpan.FromHours(12), TimeSpan.FromHours(16), 6),

        // Dr. Mostafa Ali (Dentist) - InstructorId = 3

        AddSchedule(3, DayOfWeek.Monday,   TimeSpan.FromHours(15), TimeSpan.FromHours(20), 15),
        AddSchedule(3, DayOfWeek.Tuesday,  TimeSpan.FromHours(9), TimeSpan.FromHours(13), 4),
        AddSchedule(3, DayOfWeek.Wednesday,TimeSpan.FromHours(9), TimeSpan.FromHours(13), 4),
        AddSchedule(3, DayOfWeek.Friday, TimeSpan.FromHours(14),TimeSpan.FromHours(18), 5),

        // Dr. Nada Ibrah (Pediatrician) - InstructorId = 4


        AddSchedule(4, DayOfWeek.Sunday,   TimeSpan.FromHours(8), TimeSpan.FromHours(12), 7),
        AddSchedule(4, DayOfWeek.Friday,   TimeSpan.FromHours(14),TimeSpan.FromHours(18), 6),
        AddSchedule(4, DayOfWeek.Wednesday,TimeSpan.FromHours(15), TimeSpan.FromHours(20), 15),
        AddSchedule(4, DayOfWeek.Thursday, TimeSpan.FromHours(10),TimeSpan.FromHours(14), 6),

        // Dr. Omar Khaled (Orthopedic) - InstructorId = 5

        AddSchedule(5, DayOfWeek.Sunday,   TimeSpan.FromHours(11), TimeSpan.FromHours(15), 6),
        AddSchedule(5, DayOfWeek.Tuesday,  TimeSpan.FromHours(18), TimeSpan.FromHours(19), 4),
        AddSchedule(5, DayOfWeek.Thursday, TimeSpan.FromHours(9), TimeSpan.FromHours(13), 10),
        AddSchedule(5, DayOfWeek.Monday,   TimeSpan.FromHours(16),TimeSpan.FromHours(20), 8),

        // Dr. Mariam Adel (Gynecologist) - InstructorId = 6


        AddSchedule(6, DayOfWeek.Sunday,   TimeSpan.FromHours(9), TimeSpan.FromHours(13), 10),
        AddSchedule(6, DayOfWeek.Monday,   TimeSpan.FromHours(9), TimeSpan.FromHours(13), 10),
        AddSchedule(6, DayOfWeek.Wednesday,TimeSpan.FromHours(14),TimeSpan.FromHours(18), 5),
        AddSchedule(6, DayOfWeek.Thursday, TimeSpan.FromHours(9), TimeSpan.FromHours(13), 8),

        // Dr. Youssef Samir (Neurologist) - InstructorId = 7


        AddSchedule(7, DayOfWeek.Tuesday,  TimeSpan.FromHours(10), TimeSpan.FromHours(14), 5),
        AddSchedule(7, DayOfWeek.Wednesday,TimeSpan.FromHours(10), TimeSpan.FromHours(14), 5),
        AddSchedule(7, DayOfWeek.Monday,   TimeSpan.FromHours(10), TimeSpan.FromHours(14), 5),

        // Dr. Reem Tarek (Psychiatrist) - InstructorId = 8

        AddSchedule(8, DayOfWeek.Friday,  TimeSpan.FromHours(12), TimeSpan.FromHours(16), 4),
        AddSchedule(8, DayOfWeek.Wednesday,TimeSpan.FromHours(16),TimeSpan.FromHours(20), 4),
        AddSchedule(8, DayOfWeek.Saturday,TimeSpan.FromHours(16),TimeSpan.FromHours(20), 4),


        // Dr. Hany Fathy (Ophthalmologist) - InstructorId = 9

        AddSchedule(9, DayOfWeek.Sunday,   TimeSpan.FromHours(9), TimeSpan.FromHours(13), 6),
        AddSchedule(9, DayOfWeek.Monday,   TimeSpan.FromHours(14),TimeSpan.FromHours(18), 5),
        AddSchedule(9, DayOfWeek.Tuesday,  TimeSpan.FromHours(9), TimeSpan.FromHours(13), 6),
        AddSchedule(9, DayOfWeek.Wednesday,TimeSpan.FromHours(9), TimeSpan.FromHours(13), 6),
        AddSchedule(9, DayOfWeek.Saturday,TimeSpan.FromHours(9), TimeSpan.FromHours(13), 6),


        // Dr. Salma Nabil (GP) - InstructorId = 10

        AddSchedule(10, DayOfWeek.Monday,   TimeSpan.FromHours(9), TimeSpan.FromHours(13), 8),
        AddSchedule(10, DayOfWeek.Friday,  TimeSpan.FromHours(9), TimeSpan.FromHours(13), 8),
        AddSchedule(10, DayOfWeek.Tuesday,TimeSpan.FromHours(9), TimeSpan.FromHours(13), 8),
        AddSchedule(10, DayOfWeek.Thursday, TimeSpan.FromHours(9), TimeSpan.FromHours(13), 8),
        AddSchedule(10, DayOfWeek.Sunday,   TimeSpan.FromHours(16),TimeSpan.FromHours(20), 6),

    
    // =========================
    // Body Gym
    // =========================

    // Captain Ahmed Samy
    AddSchedule(14, DayOfWeek.Sunday, TimeSpan.FromHours(10), TimeSpan.FromHours(14), 15),
    AddSchedule(14, DayOfWeek.Tuesday, TimeSpan.FromHours(17), TimeSpan.FromHours(21), 12),

    // Captain Nour Ali
    AddSchedule(15, DayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(13), 10),
    AddSchedule(15, DayOfWeek.Wednesday, TimeSpan.FromHours(18), TimeSpan.FromHours(21), 8),

    // Captain Mostafa Adel
    AddSchedule(16, DayOfWeek.Saturday, TimeSpan.FromHours(16), TimeSpan.FromHours(20), 20),
    AddSchedule(16, DayOfWeek.Monday, TimeSpan.FromHours(17), TimeSpan.FromHours(21), 18),

    // Captain Omar Khaled
    AddSchedule(17, DayOfWeek.Sunday, TimeSpan.FromHours(18), TimeSpan.FromHours(21), 14),
    AddSchedule(17, DayOfWeek.Thursday, TimeSpan.FromHours(10), TimeSpan.FromHours(14), 12),

    // Captain Youssef Nabil
    AddSchedule(18, DayOfWeek.Tuesday, TimeSpan.FromHours(11), TimeSpan.FromHours(15), 10),


    // =========================
    // Smart Learn Center
    // =========================

    // Mr. Ahmed Hassan
    AddSchedule(19, DayOfWeek.Saturday, TimeSpan.FromHours(14), TimeSpan.FromHours(18), 25),
    AddSchedule(19, DayOfWeek.Tuesday, TimeSpan.FromHours(16), TimeSpan.FromHours(20), 20),

    // Ms. Sara Mohamed
    AddSchedule(20, DayOfWeek.Sunday, TimeSpan.FromHours(12), TimeSpan.FromHours(16), 18),
    AddSchedule(20, DayOfWeek.Wednesday, TimeSpan.FromHours(17), TimeSpan.FromHours(20), 15),
    AddSchedule(20, DayOfWeek.Thursday, TimeSpan.FromHours(12), TimeSpan.FromHours(16), 18),

    // Mr. Mostafa Ali
    AddSchedule(21, DayOfWeek.Monday, TimeSpan.FromHours(15), TimeSpan.FromHours(19), 22),
    AddSchedule(21, DayOfWeek.Thursday, TimeSpan.FromHours(10), TimeSpan.FromHours(13), 15),

    // Ms. Nada Ibrahim
    AddSchedule(22, DayOfWeek.Sunday, TimeSpan.FromHours(9), TimeSpan.FromHours(12), 16),

    // Mr. Omar Khaled
    AddSchedule(23, DayOfWeek.Tuesday, TimeSpan.FromHours(14), TimeSpan.FromHours(18), 20),
    AddSchedule(23, DayOfWeek.Friday, TimeSpan.FromHours(10), TimeSpan.FromHours(13), 12),

    // Ms. Mariam Adel
    AddSchedule(24, DayOfWeek.Saturday, TimeSpan.FromHours(18), TimeSpan.FromHours(22), 30),
    AddSchedule(24, DayOfWeek.Monday, TimeSpan.FromHours(10), TimeSpan.FromHours(13), 15),

    // Mr. Youssef Samir
    AddSchedule(25, DayOfWeek.Wednesday, TimeSpan.FromHours(12), TimeSpan.FromHours(16), 18),
    AddSchedule(25, DayOfWeek.Saturday, TimeSpan.FromHours(12), TimeSpan.FromHours(16), 18),


    // Ms. Reem Tarek
    AddSchedule(26, DayOfWeek.Sunday, TimeSpan.FromHours(17), TimeSpan.FromHours(20), 14),
    AddSchedule(26, DayOfWeek.Thursday, TimeSpan.FromHours(11), TimeSpan.FromHours(14), 10),

    // Mr. Hany Fathy
    AddSchedule(27, DayOfWeek.Monday, TimeSpan.FromHours(12), TimeSpan.FromHours(15), 18),
    AddSchedule(27, DayOfWeek.Saturday, TimeSpan.FromHours(12), TimeSpan.FromHours(16), 18),


    // Ms. Salma Nabil
    AddSchedule(28, DayOfWeek.Tuesday, TimeSpan.FromHours(18), TimeSpan.FromHours(21), 25),
    AddSchedule(28, DayOfWeek.Friday, TimeSpan.FromHours(14), TimeSpan.FromHours(18), 25),


    // =========================
    // Flash Studio
    // =========================

    // Ahmed Samy
    AddSchedule(29, DayOfWeek.Friday, TimeSpan.FromHours(16), TimeSpan.FromHours(22), 5),
    AddSchedule(29, DayOfWeek.Saturday, TimeSpan.FromHours(14), TimeSpan.FromHours(20), 5),

    // Sara Adel
    AddSchedule(30, DayOfWeek.Monday, TimeSpan.FromHours(13), TimeSpan.FromHours(18), 7),

    // Mostafa Nabil
    AddSchedule(31, DayOfWeek.Thursday, TimeSpan.FromHours(17), TimeSpan.FromHours(22), 6),
    AddSchedule(31, DayOfWeek.Friday, TimeSpan.FromHours(12), TimeSpan.FromHours(18), 8),

    // Reem Tarek
    AddSchedule(32, DayOfWeek.Sunday, TimeSpan.FromHours(15), TimeSpan.FromHours(20), 10),
    AddSchedule(32, DayOfWeek.Tuesday, TimeSpan.FromHours(15), TimeSpan.FromHours(20), 10),


    // =========================
    // Smile Care Clinic
    // =========================

    // Dr. Kareem El-Sayed
    AddSchedule(33, DayOfWeek.Sunday, TimeSpan.FromHours(10), TimeSpan.FromHours(15), 8),
    AddSchedule(33, DayOfWeek.Wednesday, TimeSpan.FromHours(16), TimeSpan.FromHours(20), 6),
    AddSchedule(33, DayOfWeek.Thursday, TimeSpan.FromHours(11), TimeSpan.FromHours(15), 7),

    // Dr. Laila Farouk
    AddSchedule(34, DayOfWeek.Monday, TimeSpan.FromHours(12), TimeSpan.FromHours(17), 10),
    AddSchedule(34, DayOfWeek.Tuesday, TimeSpan.FromHours(10), TimeSpan.FromHours(14), 8),

    // Dr. Hossam Abdelrahman
    AddSchedule(35, DayOfWeek.Saturday, TimeSpan.FromHours(10), TimeSpan.FromHours(14), 12),
    AddSchedule(35, DayOfWeek.Wednesday, TimeSpan.FromHours(15), TimeSpan.FromHours(19), 10),
            };


            foreach (var Schedule in instructorSchedules)
            {
                var existingSchedule = await context.InstructorSchedules.FirstOrDefaultAsync(c => c.InstructorId == Schedule.InstructorId
                                                                                           && c.Day == Schedule.Day
                                                                                           &&c.StartTime==Schedule.StartTime
                                                                                           &&c.EndTime==Schedule.EndTime);

                if (existingSchedule == null)
                {
                    await context.InstructorSchedules.AddAsync(Schedule);
                    await context.SaveChangesAsync();

                }
            }


        }
    }
}
