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
                ReservedCount = 0,
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
