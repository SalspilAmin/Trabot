using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;

namespace Tradify.Infrastructure.Seeder
{
    public class InstructorReviewSeeder
    {
        private static readonly Random _random = new();

        // ✅ Instructor Categories
        private static readonly Dictionary<int, string> InstructorTypes = new()
        {
            { 1, "Medical" },
            { 2, "Medical" },
            { 3, "Dentist" },
            { 4, "Medical" },
            { 5, "Medical" },
            { 6, "Medical" },
            { 7, "Medical" },
            { 8, "Psychiatrist" },
            { 9, "Medical" },
            { 10, "Medical" },

            { 14, "Fitness" },
            { 15, "Fitness" },
            { 16, "Fitness" },
            { 17, "CrossFit" },
            { 18, "Nutrition" },

            { 19, "Education" },
            { 20, "Education" },
            { 21, "Education" },
            { 22, "Education" },
            { 23, "Education" },
            { 24, "Programming" },
            { 25, "Education" },
            { 26, "Education" },
            { 27, "Education" },
            { 28, "SAT" },

            { 29, "Photography" },
            { 30, "Photography" },
            { 31, "Photography" },
            { 32, "Creative" },

            { 33, "Dentist" },
            { 34, "Dentist" },
            { 35, "Dentist" }
        };

        // =========================================================
        // ✅ FITNESS COMMENTS
        // =========================================================

        private static readonly List<string> FitnessPositiveComments = new()
        {
            "Very motivating coach and full of energy.",
            "Amazing workout plan and great results.",
            "Helped me stay consistent and disciplined.",
            "Friendly and supportive during sessions.",
            "One of the best fitness coaches I’ve tried.",
            "Great transformation guidance.",
            "Very professional and inspiring.",
            "Sessions were intense but enjoyable.",
            "Excellent coaching style.",
            "Really helped improve my fitness level."
        };

        private static readonly List<string> FitnessNeutralComments = new()
        {
            "Workout sessions were okay overall.",
            "Training was decent but repetitive sometimes.",
            "Average coaching experience.",
            "Good energy but expected more guidance.",
            "The sessions were acceptable."
        };

        private static readonly List<string> FitnessNegativeComments = new()
        {
            "The workout plan felt generic.",
            "Not enough attention during sessions.",
            "Communication could be better.",
            "Expected more personalized coaching.",
            "Did not see the expected progress."
        };

        // =========================================================
        // ✅ DENTIST COMMENTS
        // =========================================================

        private static readonly List<string> DentistPositiveComments = new()
        {
            "Very gentle and professional dentist.",
            "Made me feel comfortable during treatment.",
            "Excellent dental care and attention.",
            "The clinic was very clean and organized.",
            "Best dental experience I’ve had.",
            "Very skilled and reassuring.",
            "Treatment was painless and smooth.",
            "Highly professional and experienced.",
            "Explained every step clearly.",
            "Amazing results after the procedure."
        };

        private static readonly List<string> DentistNeutralComments = new()
        {
            "The treatment was okay overall.",
            "Average dental experience.",
            "Service was acceptable.",
            "Nothing special but decent.",
            "The appointment went fine."
        };

        private static readonly List<string> DentistNegativeComments = new()
        {
            "The treatment was painful.",
            "Long waiting time and poor organization.",
            "Did not feel comfortable during the visit.",
            "Expected better professionalism.",
            "Very stressful experience.",
            "The results did not meet expectations.",
            "Communication was not clear enough.",
            "Would not return again."
        };

        // =========================================================
        // ✅ PSYCHIATRIST COMMENTS
        // =========================================================

        private static readonly List<string> PsychiatristPositiveComments = new()
        {
            "Very understanding and supportive.",
            "Helped me feel heard and comfortable.",
            "Excellent listener and very patient.",
            "The sessions were extremely helpful.",
            "Professional and empathetic approach.",
            "Created a safe and calm environment.",
            "Very insightful and compassionate.",
            "Helped me improve emotionally.",
            "I felt respected and understood.",
            "One of the best therapy experiences."
        };

        private static readonly List<string> PsychiatristNeutralComments = new()
        {
            "The session was okay overall.",
            "Average therapy experience.",
            "Communication was decent.",
            "The advice was somewhat helpful.",
            "The session met expectations."
        };

        private static readonly List<string> PsychiatristNegativeComments = new()
        {
            "Did not feel emotionally supported.",
            "The session felt rushed.",
            "Communication was difficult.",
            "Expected more empathy.",
            "The advice did not help much.",
            "Did not feel comfortable opening up.",
            "Very disappointing experience."
        };

        // =========================================================
        // ✅ SAT COMMENTS
        // =========================================================

        private static readonly List<string> SatPositiveComments = new()
        {
            "Great SAT strategies and explanations.",
            "Helped improve my score significantly.",
            "Very organized and knowledgeable.",
            "Excellent problem-solving techniques.",
            "The sessions were very productive.",
            "Made difficult concepts easier to understand.",
            "Very supportive and motivating.",
            "Practice materials were extremely useful.",
            "Explained everything clearly.",
            "Helped reduce exam stress."
        };

        private static readonly List<string> SatNeutralComments = new()
        {
            "The classes were okay overall.",
            "Average SAT preparation experience.",
            "Some lessons were helpful.",
            "The instructor was decent.",
            "Good but expected more practice."
        };

        private static readonly List<string> SatNegativeComments = new()
        {
            "The explanations were confusing sometimes.",
            "Too much pressure during sessions.",
            "Did not improve my score as expected.",
            "The sessions felt stressful.",
            "Not enough personalized support.",
            "Expected better preparation quality."
        };

        // =========================================================
        // ✅ PHOTOGRAPHY COMMENTS
        // =========================================================

        private static readonly List<string> PhotographyPositiveComments = new()
        {
            "Amazing photography skills.",
            "The photos turned out beautiful.",
            "Very creative and professional.",
            "Excellent attention to detail.",
            "Captured every moment perfectly.",
            "Great editing and quality.",
            "Very friendly and easy to work with.",
            "The final results exceeded expectations.",
            "Highly talented photographer.",
            "Would definitely book again."
        };

        private static readonly List<string> PhotographyNeutralComments = new()
        {
            "The photos were decent overall.",
            "Average photography experience.",
            "Good quality but nothing unique.",
            "The session was acceptable.",
            "Delivery time was reasonable."
        };

        private static readonly List<string> PhotographyNegativeComments = new()
        {
            "The photos lacked creativity.",
            "Expected better editing quality.",
            "Communication was not smooth.",
            "Some photos were disappointing.",
            "Delivery took longer than expected."
        };

        // =========================================================
        // ✅ DEFAULT COMMENTS
        // =========================================================

        private static readonly List<string> DefaultPositiveComments = new()
        {
            "Excellent experience overall.",
            "Very professional and friendly.",
            "Highly recommended.",
            "Great communication and quality.",
            "Would definitely book again."
        };

        private static readonly List<string> DefaultNeutralComments = new()
        {
            "The experience was okay overall.",
            "Average service.",
            "Everything went fine.",
            "The session was acceptable.",
            "Nothing special but decent."
        };

        private static readonly List<string> DefaultNegativeComments = new()
        {
            "The experience was disappointing.",
            "Expected better professionalism.",
            "Poor communication.",
            "Would not recommend.",
            "Needs significant improvement."
        };

        // =========================================================
        // ✅ ADD REVIEW
        // =========================================================

        private static Reviews AddReview(
            int customerId,
            int instructorId)
        {
            var rating = GenerateRating();

            return new Reviews
            {
                CustomerId = customerId,
                InstructorId = instructorId,

                Rating = rating,

                // ✅ comment based on instructor type + rating
                Comment = GenerateComment(instructorId, rating),

                IsPurchased = _random.NextDouble() > 0.1,

                CreatedAt = DateTime.UtcNow
                    .AddDays(-_random.Next(1, 180))
                    .AddMinutes(-_random.Next(1, 1440))
            };
        }

        // =========================================================
        // ✅ RATING GENERATOR
        // =========================================================

        private static RatingValue GenerateRating()
        {
            int random = _random.Next(1, 101);

            return random switch
            {
                <= 3 => RatingValue.VeryBad,
                <= 10 => RatingValue.Bad,
                <= 25 => RatingValue.Average,
                <= 65 => RatingValue.Good,
                _ => RatingValue.Excellent
            };
        }

        // =========================================================
        // ✅ COMMENT GENERATOR
        // =========================================================

        private static string GenerateComment(
            int instructorId,
            RatingValue rating)
        {
            var type = InstructorTypes.ContainsKey(instructorId)
                ? InstructorTypes[instructorId]
                : "Default";

            bool isPositive =
                rating == RatingValue.Good ||
                rating == RatingValue.Excellent;

            bool isNegative =
                rating == RatingValue.Bad ||
                rating == RatingValue.VeryBad;

            return type switch
            {
                "Fitness" => GetComment(
                    isPositive ? FitnessPositiveComments :
                    isNegative ? FitnessNegativeComments :
                    FitnessNeutralComments),

                "Dentist" => GetComment(
                    isPositive ? DentistPositiveComments :
                    isNegative ? DentistNegativeComments :
                    DentistNeutralComments),

                "Psychiatrist" => GetComment(
                    isPositive ? PsychiatristPositiveComments :
                    isNegative ? PsychiatristNegativeComments :
                    PsychiatristNeutralComments),

                "SAT" => GetComment(
                    isPositive ? SatPositiveComments :
                    isNegative ? SatNegativeComments :
                    SatNeutralComments),

                "Photography" => GetComment(
                    isPositive ? PhotographyPositiveComments :
                    isNegative ? PhotographyNegativeComments :
                    PhotographyNeutralComments),

                _ => GetComment(
                    isPositive ? DefaultPositiveComments :
                    isNegative ? DefaultNegativeComments :
                    DefaultNeutralComments)
            };
        }

        private static string GetComment(List<string> comments)
        {
            return comments[_random.Next(comments.Count)];
        }

        // =========================================================
        // ✅ SEED
        // =========================================================

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var reviews = new List<Reviews>();

            var customerIds = Enumerable.Range(73, 50).ToList();

            var instructorIds = new List<int>
            {
                1,2,3,4,5,6,7,8,9,10,
                14,15,16,17,18,
                19,20,21,22,23,24,25,26,27,28,
                29,30,31,32,
                33,34,35
            };

            var existingReviews = await context.Reviews
                .Select(r => new
                {
                    r.CustomerId,
                    r.InstructorId
                })
                .ToListAsync();

            var existingSet = existingReviews
                .Select(x => $"{x.CustomerId}-{x.InstructorId}")
                .ToHashSet();

            foreach (var customerId in customerIds)
            {
                int reviewsCount = _random.Next(5, 16);

                var selectedInstructors = instructorIds
                    .OrderBy(x => Guid.NewGuid())
                    .Take(reviewsCount)
                    .ToList();

                foreach (var instructorId in selectedInstructors)
                {
                    var key = $"{customerId}-{instructorId}";

                    if (existingSet.Contains(key))
                        continue;

                    reviews.Add(AddReview(customerId, instructorId));

                    existingSet.Add(key);
                }
            }

            if (reviews.Any())
            {
                await context.Reviews.AddRangeAsync(reviews);

                await context.SaveChangesAsync();
            }
        }
    }
}