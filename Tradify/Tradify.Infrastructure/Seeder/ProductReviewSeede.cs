using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Tradify.Infrastructure.Seeder
{
    public class ProductReviewSeeder
    {
        private static readonly Random _random = new();

        // =========================================================
        // ✅ PRODUCT CATEGORIES
        // =========================================================

        private static readonly Dictionary<int, string> ProductTypes = new()
        {
            // Fashion
            { 1, "Fashion" },
            { 2, "Fashion" },
            { 3, "Fashion" },
            { 4, "Fashion" },

            // Flowers
            { 5, "Flowers" },
            { 6, "Flowers" },
            { 7, "Flowers" },
            { 8, "Accessories" },

            // Home
            { 9, "Furniture" },
            { 10, "Decor" },
            { 11, "Kitchen" },
            { 12, "Electronics" },
            { 13, "Cleaning" },

            // Market
            { 15, "Dairy" },
            { 16, "Food" },
            { 17, "Snacks" },
            { 18, "Drinks" },
            { 32, "Food" },
            { 33, "Food" },

            // Tech
            { 19, "Phones" },
            { 20, "Laptops" },
            { 21, "TechAccessories" },
            { 22, "SmartDevices" },

            // Beauty
            { 23, "Beauty" },
            { 24, "Beauty" },
            { 25, "Beauty" },
            { 26, "Perfume" },
            { 27, "Beauty" },

            // Health
            { 28, "Medicine" },
            { 29, "Medicine" },
            { 30, "MedicalSupplies" },
            { 31, "BabyCare" }
        };

        // =========================================================
        // ✅ FASHION COMMENTS
        // =========================================================

        private static readonly List<string> FashionPositiveComments = new()
        {
            "Amazing quality and perfect fit.",
            "The material feels premium.",
            "Very stylish and comfortable.",
            "Exactly like the pictures.",
            "Loved the design and quality.",
            "Will definitely order again.",
            "Great value for money.",
            "Fast delivery and excellent product.",
            "The fabric is really soft.",
            "Looks even better in real life."
        };

        private static readonly List<string> FashionNeutralComments = new()
        {
            "The product was okay overall.",
            "Average quality.",
            "Good but expected better material.",
            "The size was slightly different.",
            "Decent experience overall."
        };

        private static readonly List<string> FashionNegativeComments = new()
        {
            "The quality was disappointing.",
            "Size was inaccurate.",
            "Material felt cheap.",
            "Not worth the price.",
            "The product looked different than expected."
        };

        // =========================================================
        // ✅ FLOWERS COMMENTS
        // =========================================================

        private static readonly List<string> FlowersPositiveComments = new()
        {
            "Beautiful flower arrangement.",
            "The flowers were fresh and elegant.",
            "Perfect gift for special occasions.",
            "Smelled amazing and lasted long.",
            "Packaging was very luxurious.",
            "Exactly what I wanted.",
            "The bouquet looked stunning.",
            "Very creative arrangement.",
            "Delivery was fast and flowers were fresh.",
            "Highly recommended florist."
        };

        private static readonly List<string> FlowersNeutralComments = new()
        {
            "The flowers were decent.",
            "Average bouquet quality.",
            "Delivery was acceptable.",
            "The arrangement was okay.",
            "Good but not exceptional."
        };

        private static readonly List<string> FlowersNegativeComments = new()
        {
            "Flowers were not fresh enough.",
            "Bouquet looked smaller than expected.",
            "Delivery was delayed.",
            "The arrangement was disappointing.",
            "Some flowers were damaged."
        };

        // =========================================================
        // ✅ TECH COMMENTS
        // =========================================================

        private static readonly List<string> TechPositiveComments = new()
        {
            "Excellent performance and quality.",
            "Works perfectly with no issues.",
            "Very fast and responsive.",
            "Battery life is impressive.",
            "Highly recommended product.",
            "Amazing specs for the price.",
            "Exceeded my expectations.",
            "Very smooth user experience.",
            "Premium build quality.",
            "Worth every penny."
        };

        private static readonly List<string> TechNeutralComments = new()
        {
            "The device works fine overall.",
            "Average performance.",
            "Good but expected better battery life.",
            "The product is acceptable.",
            "Decent experience."
        };

        private static readonly List<string> TechNegativeComments = new()
        {
            "Battery drains too quickly.",
            "Performance was disappointing.",
            "Expected better quality.",
            "The product had technical issues.",
            "Not satisfied with the purchase."
        };

        // =========================================================
        // ✅ BEAUTY COMMENTS
        // =========================================================

        private static readonly List<string> BeautyPositiveComments = new()
        {
            "Amazing results after using it.",
            "Very gentle on the skin.",
            "Smells wonderful.",
            "The quality is excellent.",
            "Highly recommended beauty product.",
            "Packaging looks luxurious.",
            "Works exactly as described.",
            "My skin feels much better.",
            "Long lasting and effective.",
            "Will buy it again."
        };

        private static readonly List<string> BeautyNeutralComments = new()
        {
            "The product is okay overall.",
            "Average beauty product.",
            "Results were acceptable.",
            "Nothing special but decent.",
            "Good enough for the price."
        };

        private static readonly List<string> BeautyNegativeComments = new()
        {
            "Did not see noticeable results.",
            "The scent was too strong.",
            "Caused skin irritation.",
            "Not worth the price.",
            "Very disappointing experience."
        };

        // =========================================================
        // ✅ FOOD COMMENTS
        // =========================================================

        private static readonly List<string> FoodPositiveComments = new()
        {
            "Very fresh and delicious.",
            "Excellent quality products.",
            "Tastes amazing.",
            "Fresh and well packaged.",
            "Great supermarket quality.",
            "Would definitely buy again.",
            "Good value for money.",
            "Fresh ingredients and fast delivery.",
            "Exceeded expectations.",
            "Very satisfied with the quality."
        };

        private static readonly List<string> FoodNeutralComments = new()
        {
            "The product was acceptable.",
            "Average freshness.",
            "Good but nothing special.",
            "Packaging was okay.",
            "The quality was decent."
        };

        private static readonly List<string> FoodNegativeComments = new()
        {
            "The product was not fresh enough.",
            "Packaging was poor.",
            "Taste was disappointing.",
            "Expected better quality.",
            "Delivery took too long."
        };

        // =========================================================
        // ✅ DEFAULT COMMENTS
        // =========================================================

        private static readonly List<string> DefaultPositiveComments = new()
        {
            "Excellent product overall.",
            "Highly recommended.",
            "Great quality and service.",
            "Very satisfied with the purchase.",
            "Would buy again."
        };

        private static readonly List<string> DefaultNeutralComments = new()
        {
            "The experience was okay overall.",
            "Average product.",
            "Everything was acceptable.",
            "Decent quality.",
            "Good overall."
        };

        private static readonly List<string> DefaultNegativeComments = new()
        {
            "The product was disappointing.",
            "Expected better quality.",
            "Not satisfied overall.",
            "Would not recommend.",
            "Poor experience."
        };

        // =========================================================
        // ✅ ADD REVIEW
        // =========================================================

        private static Reviews AddReview(
            int customerId,
            int productId,
            int categoryId)
        {
            var rating = GenerateRating(categoryId);

            return new Reviews
            {
                CustomerId = customerId,

                ProductId = productId,

                Rating = rating,

                Comment = GenerateComment(categoryId, rating),

                IsPurchased = _random.NextDouble() > 0.08,

                CreatedAt = DateTime.UtcNow
                    .AddDays(-_random.Next(1, 180))
                    .AddMinutes(-_random.Next(1, 1440))
            };
        }

        // =========================================================
        // ✅ RATING GENERATOR
        // =========================================================

        private static RatingValue GenerateRating(int categoryId)
        {
            int random = _random.Next(1, 101);

            // Tech products → more mixed reviews
            if (categoryId is 19 or 20 or 21 or 22)
            {
                return random switch
                {
                    <= 8 => RatingValue.VeryBad,
                    <= 18 => RatingValue.Bad,
                    <= 35 => RatingValue.Average,
                    <= 70 => RatingValue.Good,
                    _ => RatingValue.Excellent
                };
            }

            // Beauty & Fashion → mostly positive
            if (categoryId is 1 or 2 or 23 or 24 or 25 or 26 or 27)
            {
                return random switch
                {
                    <= 2 => RatingValue.VeryBad,
                    <= 8 => RatingValue.Bad,
                    <= 20 => RatingValue.Average,
                    <= 60 => RatingValue.Good,
                    _ => RatingValue.Excellent
                };
            }

            // Default
            return random switch
            {
                <= 4 => RatingValue.VeryBad,
                <= 12 => RatingValue.Bad,
                <= 28 => RatingValue.Average,
                <= 65 => RatingValue.Good,
                _ => RatingValue.Excellent
            };
        }

        // =========================================================
        // ✅ COMMENT GENERATOR
        // =========================================================

        private static string GenerateComment(
            int categoryId,
            RatingValue rating)
        {
            var type = ProductTypes.ContainsKey(categoryId)
                ? ProductTypes[categoryId]
                : "Default";

            bool isPositive =
                rating == RatingValue.Good ||
                rating == RatingValue.Excellent;

            bool isNegative =
                rating == RatingValue.Bad ||
                rating == RatingValue.VeryBad;

            return type switch
            {
                "Fashion" => GetComment(
                    isPositive ? FashionPositiveComments :
                    isNegative ? FashionNegativeComments :
                    FashionNeutralComments),

                "Flowers" => GetComment(
                    isPositive ? FlowersPositiveComments :
                    isNegative ? FlowersNegativeComments :
                    FlowersNeutralComments),

                "Phones" or "Laptops" or "TechAccessories" or "SmartDevices"
                    => GetComment(
                    isPositive ? TechPositiveComments :
                    isNegative ? TechNegativeComments :
                    TechNeutralComments),

                "Beauty" or "Perfume"
                    => GetComment(
                    isPositive ? BeautyPositiveComments :
                    isNegative ? BeautyNegativeComments :
                    BeautyNeutralComments),

                "Food" or "Drinks" or "Dairy"
                    => GetComment(
                    isPositive ? FoodPositiveComments :
                    isNegative ? FoodNegativeComments :
                    FoodNeutralComments),

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

            var products = await context.Products
                .Where(p => p.Id >= 116 && p.Id <= 416)
                .Select(p => new
                {
                    p.Id,
                    p.CategoryId
                })
                .ToListAsync();

            var existingReviews = await context.Reviews
                .Where(r => r.ProductId != null)
                .Select(r => new
                {
                    r.CustomerId,
                    r.ProductId
                })
                .ToListAsync();

            var existingSet = existingReviews
                .Select(x => $"{x.CustomerId}-{x.ProductId}")
                .ToHashSet();

            foreach (var customerId in customerIds)
            {
                int reviewsCount = _random.Next(12, 35);

                var selectedProducts = products
                    .OrderBy(x => Guid.NewGuid())
                    .Take(reviewsCount)
                    .ToList();

                foreach (var product in selectedProducts)
                {
                    var key = $"{customerId}-{product.Id}";

                    if (existingSet.Contains(key))
                        continue;

                    reviews.Add(AddReview(
                        customerId,
                        product.Id,
                        product.CategoryId));

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
