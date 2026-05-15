using Microsoft.EntityFrameworkCore;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;

namespace Tradify.Infrastructure.Seeder
{
    public class ProductSeeder
    {
        private static Products AddProduct(
            string name,
            string description,
            int categoryId,
            int storeId,
            decimal price,
            int stock)
        {
            return new Products
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                StoreId = storeId,
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,

                ProductVariants = new List<ProductVariants>
                {
                    new ProductVariants
                    {
                        Name = name,
                        Price = price,
                        NumberOfProductInStock = stock,
                        Discount = 0,
                        Color = null,
                        Size = null
                    }
                }
            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var products = new List<Products>()
            { 
                // 🔹 Trendy Wear (StoreId = 1)


                // =========================
                // Men Clothing (CategoryId = 1)
                // =========================

                AddProduct("Classic Black T-Shirt", "Soft cotton black t-shirt", 1, 1, 450, 25),
                AddProduct("Slim Fit Jeans", "Blue slim fit jeans", 1, 1, 900, 18),
                AddProduct("Casual White Shirt", "Elegant white casual shirt", 1, 1, 700, 12),
                AddProduct("Oversized Hoodie", "Warm oversized hoodie", 1, 1, 1200, 10),
                AddProduct("Sport Sweatpants", "Comfortable sweatpants", 1, 1, 650, 20),
                AddProduct("Leather Jacket", "Stylish black leather jacket", 1, 1, 2500, 7),
                AddProduct("Polo T-Shirt", "Premium polo t-shirt", 1, 1, 550, 14),
                AddProduct("Cargo Pants", "Modern cargo pants", 1, 1, 850, 16),
                AddProduct("Denim Jacket", "Blue denim jacket", 1, 1, 1700, 8),
                AddProduct("Summer Shorts", "Light summer shorts", 1, 1, 400, 22),

                // =========================
                // Women Clothing (CategoryId = 2)
                // =========================

                AddProduct("Floral Dress", "Elegant floral dress", 2, 1, 1400, 12),
                AddProduct("Pink Hoodie", "Cute pink hoodie", 2, 1, 1100, 15),
                AddProduct("High Waist Jeans", "Comfortable high waist jeans", 2, 1, 950, 18),
                AddProduct("Basic Crop Top", "Soft crop top", 2, 1, 350, 30),
                AddProduct("Long Skirt", "Stylish long skirt", 2, 1, 750, 10),
                AddProduct("Knitted Sweater", "Warm knitted sweater", 2, 1, 1300, 9),
                AddProduct("Elegant Blazer", "Formal women blazer", 2, 1, 2100, 6),
                AddProduct("Yoga Leggings", "Flexible gym leggings", 2, 1, 600, 25),
                AddProduct("Silk Pajama Set", "Soft silk pajama", 2, 1, 1250, 8),
                AddProduct("Casual Cardigan", "Light cardigan", 2, 1, 950, 11),

                // =========================
                // Kids Wear (CategoryId = 3)
                // =========================

                AddProduct("Kids Cartoon T-Shirt", "Funny cartoon t-shirt", 3, 1, 300, 20),
                AddProduct("Kids Pajama Set", "Comfortable sleepwear", 3, 1, 500, 15),
                AddProduct("Baby Winter Jacket", "Warm baby jacket", 3, 1, 950, 7),
                AddProduct("School Uniform", "Durable school uniform", 3, 1, 700, 12),
                AddProduct("Kids Jeans", "Stretch kids jeans", 3, 1, 450, 14),
                AddProduct("Princess Dress", "Cute princess dress", 3, 1, 850, 9),
                AddProduct("Kids Hoodie", "Warm hoodie for kids", 3, 1, 650, 13),
                AddProduct("Baby Romper", "Soft cotton romper", 3, 1, 350, 18),

                // =========================
                // Shoes (CategoryId = 4)
                // =========================

                AddProduct("Nike Running Shoes", "Comfortable running shoes", 4, 1, 3200, 10),
                AddProduct("Adidas Sneakers", "Modern sneakers", 4, 1, 2800, 11),
                AddProduct("Classic Leather Shoes", "Formal leather shoes", 4, 1, 3500, 6),
                AddProduct("Women High Heels", "Elegant high heels", 4, 1, 2400, 8),
                AddProduct("Kids Sport Shoes", "Durable kids shoes", 4, 1, 1500, 14),
                AddProduct("Casual Slip On", "Easy slip-on shoes", 4, 1, 1700, 9),
                AddProduct("Canvas Shoes", "Everyday canvas shoes", 4, 1, 1300, 13),
                AddProduct("Hiking Boots", "Outdoor hiking boots", 4, 1, 4200, 5),


                // 🔹 Arms Flowers (StoreId = 2)

                 // =========================
 // Bouquets (CategoryId = 5)
 // =========================

 AddProduct("Red Roses Bouquet", "Elegant bouquet of fresh red roses", 5, 2, 850, 12),
 AddProduct("White Lily Bouquet", "Fresh white lily arrangement", 5, 2, 950, 10),
 AddProduct("Sunflower Bouquet", "Bright sunflower bouquet", 5, 2, 780, 15),
 AddProduct("Mixed Tulip Bouquet", "Colorful tulip flower bouquet", 5, 2, 1100, 8),
 AddProduct("Pink Roses Bouquet", "Soft pink roses bouquet", 5, 2, 890, 11),
 AddProduct("Luxury Orchid Bouquet", "Premium orchid flower arrangement", 5, 2, 1600, 5),
 AddProduct("Spring Flowers Bouquet", "Fresh spring mixed flowers", 5, 2, 920, 9),
 AddProduct("Romantic Roses Box", "Luxury roses in gift box", 5, 2, 1350, 7),

 // =========================
 // Wedding Flowers (CategoryId = 6)
 // =========================

 AddProduct("Wedding Roses Package", "Elegant wedding roses setup", 6, 2, 4500, 3),
 AddProduct("Bridal Bouquet", "Luxury bridal flower bouquet", 6, 2, 2200, 5),
 AddProduct("Wedding Table Flowers", "Floral table decorations", 6, 2, 1800, 6),
 AddProduct("White Wedding Arch Flowers", "Wedding arch floral arrangement", 6, 2, 5200, 2),
 AddProduct("Luxury Wedding Centerpiece", "Premium wedding centerpiece", 6, 2, 2650, 4),
 AddProduct("Garden Wedding Flowers", "Outdoor wedding flower setup", 6, 2, 6100, 2),
 AddProduct("Classic Wedding Bouquet", "Classic white wedding bouquet", 6, 2, 2400, 5),

 // =========================
 // HandMade Bouquets (CategoryId = 7)
 // =========================

 AddProduct("Handmade Lavender Bouquet", "Handcrafted lavender bouquet", 7, 2, 990, 10),
 AddProduct("Crochet Flower Bouquet", "Cute crochet handmade flowers", 7, 2, 1250, 8),
 AddProduct("Handmade Satin Roses", "Satin ribbon rose bouquet", 7, 2, 1150, 7),
 AddProduct("Mini Handmade Bouquet", "Small handmade floral gift", 7, 2, 650, 14),
 AddProduct("Knitted Tulip Bouquet", "Soft knitted tulip flowers", 7, 2, 1400, 6),
 AddProduct("Custom Handmade Bouquet", "Personalized handmade bouquet", 7, 2, 1750, 4),
 AddProduct("Handmade Baby Roses", "Cute handmade baby roses", 7, 2, 850, 12),



 // =========================
 // Accessories (CategoryId = 8)
 // =========================

 AddProduct("Flower Vase", "Elegant glass flower vase", 8, 2, 450, 18),
 AddProduct("Gift Wrapping Set", "Luxury bouquet wrapping materials", 8, 2, 220, 25),
 AddProduct("Wedding Ribbon Pack", "Decorative wedding ribbons", 8, 2, 180, 30),
 AddProduct("Flower Basket", "Classic flower basket", 8, 2, 520, 10),
 AddProduct("LED Flower Lights", "Decorative lights for bouquets", 8, 2, 350, 20),
 AddProduct("Greeting Card Set", "Beautiful greeting cards", 8, 2, 120, 40),
 AddProduct("Pearl Bouquet Pins", "Decorative pearl bouquet pins", 8, 2, 160, 35),
 AddProduct("Luxury Gift Box", "Premium flower gift box", 8, 2, 480, 15),

                 // 🔹 House Needs (StoreId = 3)

  // =========================
 // Furniture (CategoryId = 9)
 // =========================

 AddProduct("Modern Sofa", "Comfortable modern living room sofa", 9, 3, 12500, 4),
 AddProduct("Wooden Coffee Table", "Stylish wooden coffee table", 9, 3, 3200, 8),
 AddProduct("Dining Table Set", "Dining table with 6 chairs", 9, 3, 18500, 3),
 AddProduct("Office Chair", "Ergonomic office chair", 9, 3, 4200, 10),
 AddProduct("Wardrobe Closet", "Large wooden wardrobe", 9, 3, 14500, 2),
 AddProduct("TV Stand", "Modern TV stand with storage", 9, 3, 3900, 7),
 AddProduct("Bookshelf", "Minimal wooden bookshelf", 9, 3, 2600, 9),
 AddProduct("Bed Frame", "Queen size wooden bed frame", 9, 3, 11000, 5),

 // =========================
 // Home Decor (CategoryId = 10)
 // =========================

 AddProduct("Wall Art Frame", "Modern decorative wall frame", 10, 3, 850, 15),
 AddProduct("Decorative Mirror", "Luxury home mirror", 10, 3, 2400, 6),
 AddProduct("Indoor Plant Pot", "Elegant ceramic plant pot", 10, 3, 450, 20),
 AddProduct("Table Lamp", "Modern bedside table lamp", 10, 3, 980, 12),
 AddProduct("Luxury Curtains", "Premium blackout curtains", 10, 3, 2200, 8),
 AddProduct("Decorative Clock", "Minimal wall clock", 10, 3, 720, 14),
 AddProduct("Candle Holder Set", "Elegant candle holder decor", 10, 3, 550, 18),
 AddProduct("Artificial Plant", "Decorative artificial green plant", 10, 3, 670, 16),

 // =========================
 // Kitchen Tools (CategoryId = 11)
 // =========================

 AddProduct("Knife Set", "Professional kitchen knife set", 11, 3, 1400, 10),
 AddProduct("Non Stick Pan", "Premium non-stick frying pan", 11, 3, 950, 13),
 AddProduct("Blender Machine", "High speed kitchen blender", 11, 3, 3200, 5),
 AddProduct("Cooking Pot Set", "Stainless steel cookware set", 11, 3, 4100, 6),
 AddProduct("Electric Kettle", "Fast boiling electric kettle", 11, 3, 1250, 11),
 AddProduct("Cutting Board", "Wooden kitchen cutting board", 11, 3, 320, 22),
 AddProduct("Air Fryer", "Healthy cooking air fryer", 11, 3, 5200, 4),
 AddProduct("Kitchen Storage Containers", "Food storage container set", 11, 3, 680, 17),

 // =========================
 // Electronics (CategoryId = 12)
 // =========================

 AddProduct("Smart TV 55 Inch", "Ultra HD smart television", 12, 3, 28500, 3),
 AddProduct("Robot Vacuum Cleaner", "Automatic smart vacuum cleaner", 12, 3, 14500, 4),
 AddProduct("Portable Heater", "Electric room heater", 12, 3, 2300, 9),
 AddProduct("LED Desk Lamp", "Rechargeable LED desk lamp", 12, 3, 780, 20),
 AddProduct("Wireless Speaker", "Bluetooth home speaker", 12, 3, 1850, 14),
 AddProduct("Security Camera", "Smart home security camera", 12, 3, 3400, 7),
 AddProduct("Microwave Oven", "Digital microwave oven", 12, 3, 6200, 5),
 AddProduct("Smart Air Purifier", "Home air purification device", 12, 3, 7300, 4),

 // =========================
 // Cleaning Supplies (CategoryId = 13)
 // =========================

 AddProduct("Multi Surface Cleaner", "Powerful home cleaning liquid", 13, 3, 180, 40),
 AddProduct("Floor Mop Set", "360 spin floor mop", 13, 3, 950, 12),
 AddProduct("Glass Cleaner Spray", "Streak free glass cleaner", 13, 3, 120, 35),
 AddProduct("Laundry Basket", "Large plastic laundry basket", 13, 3, 420, 18),
 AddProduct("Cleaning Gloves", "Reusable rubber gloves", 13, 3, 90, 50),
 AddProduct("Vacuum Cleaner Bags", "Replacement vacuum bags", 13, 3, 260, 23),
 AddProduct("Bathroom Cleaning Brush", "Heavy duty cleaning brush", 13, 3, 140, 28),
 AddProduct("Trash Bags Pack", "Durable garbage bags pack", 13, 3, 110, 45),

                 // 🔹 Fresh Market (StoreId = 4)

  // =========================
 // Fruits (CategoryId = 32)
 // =========================

 AddProduct("Fresh Apples", "Crispy red apples", 32, 4, 120, 40),
 AddProduct("Bananas", "Fresh yellow bananas", 32, 4, 80, 50),
 AddProduct("Orange Pack", "Sweet juicy oranges", 32, 4, 95, 45),
 AddProduct("Green Grapes", "Seedless green grapes", 32, 4, 160, 30),
 AddProduct("Mango Box", "Fresh tropical mangoes", 32, 4, 220, 20),
 AddProduct("Watermelon", "Large fresh watermelon", 32, 4, 150, 18),
 AddProduct("Pineapple", "Sweet pineapple fruit", 32, 4, 130, 22),
 AddProduct("Kiwi Pack", "Imported kiwi fruits", 32, 4, 210, 15),
 AddProduct("Strawberries", "Fresh strawberry box", 32, 4, 175, 25),
 AddProduct("Peaches", "Soft sweet peaches", 32, 4, 140, 27),
 AddProduct("Avocado", "Fresh avocado fruit", 32, 4, 190, 20),
 AddProduct("Blueberries", "Premium blueberry pack", 32, 4, 260, 12),

 // =========================
 // Vegetables (CategoryId = 33)
 // =========================

 AddProduct("Tomatoes", "Fresh organic tomatoes", 33, 4, 45, 70),
 AddProduct("Potatoes", "Farm fresh potatoes", 33, 4, 35, 90),
 AddProduct("Cucumbers", "Crunchy cucumbers", 33, 4, 40, 60),
 AddProduct("Carrots", "Fresh orange carrots", 33, 4, 55, 55),
 AddProduct("Bell Peppers", "Mixed color peppers", 33, 4, 75, 40),
 AddProduct("Lettuce", "Fresh green lettuce", 33, 4, 50, 35),
 AddProduct("Onions", "Natural red onions", 33, 4, 38, 80),
 AddProduct("Garlic Pack", "Fresh garlic cloves", 33, 4, 60, 45),
 AddProduct("Broccoli", "Organic broccoli", 33, 4, 85, 25),
 AddProduct("Spinach", "Fresh spinach leaves", 33, 4, 48, 30),
 AddProduct("Eggplant", "Fresh purple eggplant", 33, 4, 52, 28),
 AddProduct("Zucchini", "Green zucchini", 33, 4, 58, 26),

 // =========================
 // Dairy Products (CategoryId = 15)
 // =========================

 AddProduct("Fresh Milk", "Full cream fresh milk", 15, 4, 42, 60),
 AddProduct("Greek Yogurt", "Healthy greek yogurt", 15, 4, 55, 40),
 AddProduct("Cheddar Cheese", "Premium cheddar cheese", 15, 4, 135, 22),
 AddProduct("Butter Pack", "Natural creamy butter", 15, 4, 88, 30),
 AddProduct("Chocolate Milk", "Kids chocolate milk", 15, 4, 32, 50),
 AddProduct("Mozzarella Cheese", "Pizza mozzarella cheese", 15, 4, 145, 18),
 AddProduct("Cream Cheese", "Soft cream cheese spread", 15, 4, 95, 25),
 AddProduct("Vanilla Yogurt", "Sweet vanilla yogurt", 15, 4, 28, 45),
 AddProduct("Cooking Cream", "Rich cooking cream", 15, 4, 65, 35),
 AddProduct("Feta Cheese", "Traditional feta cheese", 15, 4, 120, 20),

 // =========================
 // Meat & Poultry (CategoryId = 16)
 // =========================

 AddProduct("Fresh Chicken Breast", "Boneless chicken breast", 16, 4, 240, 25),
 AddProduct("Whole Chicken", "Fresh whole chicken", 16, 4, 310, 18),
 AddProduct("Beef Steak", "Premium beef steak", 16, 4, 480, 12),
 AddProduct("Minced Meat", "Fresh minced beef", 16, 4, 360, 16),
 AddProduct("Turkey Slices", "Healthy turkey slices", 16, 4, 220, 20),
 AddProduct("Chicken Wings", "Fresh chicken wings", 16, 4, 180, 24),
 AddProduct("Sausage Pack", "Smoked beef sausage", 16, 4, 145, 30),
 AddProduct("Burger Patties", "Frozen burger patties", 16, 4, 260, 14),
 AddProduct("Lamb Chops", "Fresh lamb chops", 16, 4, 520, 8),
 AddProduct("Chicken Nuggets", "Frozen chicken nuggets", 16, 4, 175, 22),

 // =========================
 // Snacks (CategoryId = 17)
 // =========================

 AddProduct("Potato Chips", "Crunchy salted chips", 17, 4, 25, 100),
 AddProduct("Chocolate Cookies", "Soft chocolate cookies", 17, 4, 38, 75),
 AddProduct("Popcorn Pack", "Butter flavored popcorn", 17, 4, 30, 60),
 AddProduct("Mixed Nuts", "Healthy mixed nuts", 17, 4, 110, 35),
 AddProduct("Protein Bar", "Chocolate protein snack", 17, 4, 55, 40),
 AddProduct("Pretzels", "Crispy baked pretzels", 17, 4, 42, 50),
 AddProduct("Wafer Biscuits", "Vanilla wafer biscuits", 17, 4, 27, 70),
 AddProduct("Nachos", "Cheese flavored nachos", 17, 4, 48, 45),
 AddProduct("Candy Pack", "Assorted fruit candies", 17, 4, 20, 90),
 AddProduct("Rice Cakes", "Healthy rice cakes", 17, 4, 65, 28),
 AddProduct("Granola Bars", "Oats and honey bars", 17, 4, 58, 36),
 AddProduct("Mini Cupcakes", "Chocolate mini cupcakes", 17, 4, 72, 25),


 // =========================
 // Drinks (CategoryId = 18)
 // =========================

 AddProduct("Orange Juice", "Fresh orange juice", 18, 4, 38, 50),
 AddProduct("Cola Drink", "Classic cola soft drink", 18, 4, 22, 90),
 AddProduct("Mineral Water", "Pure mineral water", 18, 4, 12, 150),
 AddProduct("Energy Drink", "High caffeine energy drink", 18, 4, 48, 40),
 AddProduct("Iced Coffee", "Cold brew iced coffee", 18, 4, 55, 35),
 AddProduct("Mango Juice", "Natural mango juice", 18, 4, 42, 45),
 AddProduct("Sparkling Water", "Refreshing sparkling water", 18, 4, 24, 60),
 AddProduct("Chocolate Shake", "Ready chocolate milkshake", 18, 4, 65, 25),
 AddProduct("Green Tea Bottle", "Healthy green tea drink", 18, 4, 35, 30),
 AddProduct("Lemon Soda", "Refreshing lemon soda", 18, 4, 26, 55),
 AddProduct("Protein Shake", "Vanilla protein drink", 18, 4, 95, 20),
 AddProduct("Apple Juice", "Fresh apple juice", 18, 4, 40, 48),

                 // 🔹 Tech Hub (StoreId = 5)
                  // =========================
 // Mobile Phones (CategoryId = 19)
 // =========================

 AddProduct("iPhone 15 Pro", "Apple flagship smartphone", 19, 5, 61900, 8),
 AddProduct("Samsung Galaxy S25", "Samsung premium Android phone", 19, 5, 54000, 10),
 AddProduct("Xiaomi Redmi Note 14", "Affordable performance smartphone", 19, 5, 14500, 19),
 AddProduct("Google Pixel 9", "Google AI powered smartphone", 19, 5, 48000, 7),
 AddProduct("OnePlus 13", "Fast and smooth Android phone", 19, 5, 39000, 9),
 AddProduct("Realme GT Neo", "Gaming performance smartphone", 19, 5, 21000, 14),
 AddProduct("Oppo Reno Series", "Stylish camera smartphone", 19, 5, 26500, 12),
 AddProduct("Huawei Nova", "Elegant Huawei smartphone", 19, 5, 23500, 11),
 AddProduct("Nokia XR", "Durable rugged smartphone", 19, 5, 17000, 6),
 AddProduct("Infinix Zero Ultra", "Large display smartphone", 19, 5, 16000, 15),

 // =========================
 // Laptops (CategoryId = 20)
 // =========================

 AddProduct("MacBook Air M4", "Lightweight Apple laptop", 20, 5, 78000, 5),
 AddProduct("Dell XPS 15", "Premium productivity laptop", 20, 5, 69000, 6),
 AddProduct("HP Victus Gaming", "Gaming laptop with RTX graphics", 20, 5, 52000, 8),
 AddProduct("Lenovo Legion 5", "High performance gaming laptop", 20, 5, 61000, 7),
 AddProduct("Asus ROG Strix", "Advanced gaming laptop", 20, 5, 73000, 4),
 AddProduct("Acer Aspire 7", "Affordable multitasking laptop", 20, 5, 34000, 10),
 AddProduct("Huawei MateBook", "Slim modern laptop", 20, 5, 47000, 6),
 AddProduct("MSI Creator Laptop", "Laptop for creators and editing", 20, 5, 82000, 3),
 AddProduct("Lenovo IdeaPad", "Everyday work laptop", 20, 5, 29500, 12),
 AddProduct("HP Pavilion", "Stylish performance laptop", 20, 5, 41000, 9),

 // =========================
 // Accessories (CategoryId = 21)
 // =========================

 AddProduct("Wireless Mouse", "Ergonomic wireless mouse", 21, 5, 650, 30),
 AddProduct("Mechanical Keyboard", "RGB mechanical gaming keyboard", 21, 5, 2400, 17),
 AddProduct("USB-C Charger", "Fast charging adapter", 21, 5, 850, 25),
 AddProduct("Bluetooth Headphones", "Noise cancelling headphones", 21, 5, 3200, 14),
 AddProduct("Phone Case", "Protective silicone phone case", 21, 5, 280, 50),
 AddProduct("Laptop Backpack", "Water resistant laptop bag", 21, 5, 1400, 20),
 AddProduct("Portable SSD", "High speed portable storage", 21, 5, 4200, 10),
 AddProduct("Webcam HD", "Full HD webcam", 21, 5, 1700, 16),
 AddProduct("Gaming Mouse Pad", "Large RGB mouse pad", 21, 5, 550, 28),
 AddProduct("Wireless Earbuds", "Compact Bluetooth earbuds", 21, 5, 2600, 21),
 AddProduct("Laptop Stand", "Adjustable aluminum laptop stand", 21, 5, 980, 15),
 AddProduct("Screen Protector", "Tempered glass protection", 21, 5, 180, 60),

 // =========================
 // Smart Devices (CategoryId = 22)
 // =========================

 AddProduct("Smart Watch", "Fitness and health smart watch", 22, 5, 5800, 18),
 AddProduct("Smart Home Camera", "WiFi security camera", 22, 5, 3400, 12),
 AddProduct("Smart Bulb", "Voice controlled LED bulb", 22, 5, 420, 35),
 AddProduct("Smart Speaker", "AI voice assistant speaker", 22, 5, 3900, 10),
 AddProduct("Fitness Tracker", "Daily activity tracking band", 22, 5, 2200, 20),
 AddProduct("Smart Door Lock", "Fingerprint smart lock", 22, 5, 7600, 6),
 AddProduct("Robot Vacuum", "Automatic smart vacuum cleaner", 22, 5, 14500, 5),
 AddProduct("Smart Thermostat", "Temperature control device", 22, 5, 5200, 8),
 AddProduct("Video Doorbell", "Smart video bell camera", 22, 5, 4800, 9),
 AddProduct("Smart Plug", "Remote control smart plug", 22, 5, 350, 40),

                 // 🔹 Beauty Care (StoreId = 6)
                  // =========================
 // Makeup (CategoryId = 23)
 // =========================

 AddProduct("Matte Lipstick", "Long lasting matte lipstick", 23, 6, 420, 35),
 AddProduct("Liquid Foundation", "Full coverage foundation", 23, 6, 680, 28),
 AddProduct("Eyeshadow Palette", "Professional makeup palette", 23, 6, 950, 18),
 AddProduct("Waterproof Mascara", "Long lasting waterproof mascara", 23, 6, 390, 30),
 AddProduct("Blush Powder", "Soft pink blush powder", 23, 6, 320, 25),
 AddProduct("Makeup Brush Set", "Professional makeup brushes", 23, 6, 780, 20),
 AddProduct("Concealer Stick", "High coverage concealer", 23, 6, 360, 22),
 AddProduct("Lip Gloss", "Shiny moisturizing lip gloss", 23, 6, 250, 40),
 AddProduct("Compact Powder", "Smooth face compact powder", 23, 6, 540, 23),
 AddProduct("Eyeliner Pen", "Precision black eyeliner", 23, 6, 280, 33),

 // =========================
 // Skincare (CategoryId = 24)
 // =========================

 AddProduct("Vitamin C Serum", "Brightening facial serum", 24, 6, 850, 18),
 AddProduct("Hydrating Moisturizer", "Deep hydration cream", 24, 6, 620, 20),
 AddProduct("Face Cleanser", "Gentle daily cleanser", 24, 6, 390, 30),
 AddProduct("Sunscreen SPF 50", "High protection sunscreen", 24, 6, 540, 27),
 AddProduct("Night Repair Cream", "Skin repair overnight cream", 24, 6, 980, 14),
 AddProduct("Hyaluronic Acid Serum", "Hydration boosting serum", 24, 6, 920, 16),
 AddProduct("Face Scrub", "Exfoliating face scrub", 24, 6, 310, 26),
 AddProduct("Sheet Mask Pack", "Refreshing sheet masks", 24, 6, 220, 40),
 AddProduct("Under Eye Cream", "Dark circle eye cream", 24, 6, 480, 19),
 AddProduct("Facial Toner", "Refreshing skin toner", 24, 6, 350, 24),

 // =========================
 // Hair Care (CategoryId = 25)
 // =========================

 AddProduct("Argan Oil Shampoo", "Nourishing hair shampoo", 25, 6, 420, 24),
 AddProduct("Hair Conditioner", "Softening hair conditioner", 25, 6, 390, 22),
 AddProduct("Hair Mask", "Deep repair hair mask", 25, 6, 580, 18),
 AddProduct("Hair Serum", "Anti-frizz hair serum", 25, 6, 460, 20),
 AddProduct("Curl Cream", "Defined curls styling cream", 25, 6, 340, 17),
 AddProduct("Hair Dryer", "Professional hair dryer", 25, 6, 2400, 9),
 AddProduct("Heat Protection Spray", "Protective styling spray", 25, 6, 370, 21),
 AddProduct("Scalp Treatment Oil", "Healthy scalp oil", 25, 6, 520, 15),
 AddProduct("Hair Brush Set", "Premium hair brushes", 25, 6, 650, 14),
 AddProduct("Keratin Treatment Kit", "Hair smoothing treatment", 25, 6, 1450, 8),

 // =========================
 // Perfume (CategoryId = 26)
 // =========================

 AddProduct("Rose Bloom Perfume", "Elegant floral fragrance", 26, 6, 1800, 12),
 AddProduct("Vanilla Mist", "Sweet vanilla perfume", 26, 6, 1450, 15),
 AddProduct("Oud Royal", "Luxury oud fragrance", 26, 6, 3200, 7),
 AddProduct("Fresh Citrus Perfume", "Refreshing citrus scent", 26, 6, 1600, 11),
 AddProduct("Midnight Secret", "Long lasting evening perfume", 26, 6, 2400, 9),
 AddProduct("Ocean Breeze", "Cool aquatic fragrance", 26, 6, 1700, 13),
 AddProduct("Soft Musk", "Warm musk perfume", 26, 6, 2100, 10),
 AddProduct("Cherry Blossom", "Light floral perfume", 26, 6, 1550, 14),

 // =========================
 // Personal Care (CategoryId = 27)
 // =========================

 AddProduct("Body Lotion", "Moisturizing body lotion", 27, 6, 340, 30),
 AddProduct("Shower Gel", "Refreshing shower gel", 27, 6, 260, 35),
 AddProduct("Deodorant Spray", "Long lasting deodorant", 27, 6, 190, 40),
 AddProduct("Hand Cream", "Softening hand cream", 27, 6, 220, 32),
 AddProduct("Body Scrub", "Exfoliating body scrub", 27, 6, 410, 20),
 AddProduct("Toothpaste Pack", "Whitening toothpaste", 27, 6, 120, 50),
 AddProduct("Electric Toothbrush", "Rechargeable toothbrush", 27, 6, 1350, 12),
 AddProduct("Feminine Care Kit", "Daily feminine hygiene kit", 27, 6, 270, 27),
 AddProduct("Cotton Pads", "Soft cosmetic cotton pads", 27, 6, 90, 60),
 AddProduct("Hand Sanitizer", "Antibacterial sanitizer gel", 27, 6, 110, 45),

                // 🔹 Health Care (StoreId = 7)
  // =========================
 // Medicines (CategoryId = 28)
 // =========================

 AddProduct("Paracetamol Tablets", "Pain relief and fever reducer", 28, 7, 45, 80),
 AddProduct("Ibuprofen Capsules", "Anti inflammatory pain relief", 28, 7, 65, 60),
 AddProduct("Cold & Flu Syrup", "Relief for cold symptoms", 28, 7, 85, 40),
 AddProduct("Cough Lozenges", "Soothing throat lozenges", 28, 7, 35, 90),
 AddProduct("Allergy Relief Tablets", "Daily allergy medication", 28, 7, 120, 35),
 AddProduct("Antacid Chewables", "Fast heartburn relief", 28, 7, 55, 50),
 AddProduct("Nasal Spray", "Blocked nose relief spray", 28, 7, 95, 28),
 AddProduct("Pain Relief Gel", "Muscle pain relief gel", 28, 7, 110, 22),
 AddProduct("Vitamin C Effervescent", "Immune support tablets", 28, 7, 130, 30),
 AddProduct("Digestive Support Capsules", "Digestive health supplement", 28, 7, 145, 18),

 // =========================
 // Vitamins (CategoryId = 29)
 // =========================

 AddProduct("Multivitamin Capsules", "Daily complete multivitamin", 29, 7, 220, 25),
 AddProduct("Vitamin D3", "Bone and immune support", 29, 7, 180, 29),
 AddProduct("Omega 3 Fish Oil", "Heart health supplement", 29, 7, 340, 20),
 AddProduct("Iron Supplement", "Supports healthy iron levels", 29, 7, 160, 24),
 AddProduct("Calcium Tablets", "Strong bones support", 29, 7, 210, 22),
 AddProduct("Magnesium Capsules", "Muscle and nerve support", 29, 7, 240, 18),
 AddProduct("Zinc Tablets", "Immune system support", 29, 7, 129, 26),
 AddProduct("Biotin Gummies", "Hair and nails vitamins", 29, 7, 290, 15),
 AddProduct("Kids Multivitamin", "Daily vitamins for children", 29, 7, 175, 20),
 AddProduct("Vitamin B Complex", "Energy support vitamins", 29, 7, 250, 17),

 // =========================
 // Medical Supplies (CategoryId = 30)
 // =========================

 AddProduct("Digital Thermometer", "Fast temperature measurement", 30, 7, 320, 18),
 AddProduct("Blood Pressure Monitor", "Automatic BP monitor", 30, 7, 1850, 10),
 AddProduct("First Aid Kit", "Complete emergency first aid kit", 30, 7, 520, 16),
 AddProduct("Medical Face Masks", "Disposable protective masks", 30, 7, 90, 100),
 AddProduct("Hand Gloves", "Disposable medical gloves", 30, 7, 110, 80),
 AddProduct("Wheelchair", "Foldable mobility wheelchair", 30, 7, 6200, 4),
 AddProduct("Crutches", "Adjustable walking crutches", 30, 7, 850, 9),
 AddProduct("Nebulizer Machine", "Portable nebulizer device", 30, 7, 1450, 12),
 AddProduct("Pulse Oximeter", "Blood oxygen monitor", 30, 7, 780, 14),
 AddProduct("Medical Tape", "Strong adhesive medical tape", 30, 7, 45, 60),

 // =========================
 // Baby Care (CategoryId = 31)
 // =========================

 AddProduct("Baby Diapers Pack", "Soft absorbent diapers", 31, 7, 420, 35),
 AddProduct("Baby Shampoo", "Gentle baby hair shampoo", 31, 7, 180, 24),
 AddProduct("Baby Lotion", "Moisturizing baby lotion", 31, 7, 210, 20),
 AddProduct("Baby Wipes", "Sensitive skin baby wipes", 31, 7, 95, 50),
 AddProduct("Baby Feeding Bottle", "Anti colic feeding bottle", 31, 7, 150, 28),
 AddProduct("Baby Powder", "Soft baby body powder", 31, 7, 120, 30),
 AddProduct("Baby Oil", "Gentle baby massage oil", 31, 7, 170, 18),
 AddProduct("Pacifier Set", "Safe silicone pacifiers", 31, 7, 130, 25),
 AddProduct("Baby Rash Cream", "Diaper rash protection cream", 31, 7, 190, 16),
 AddProduct("Baby Formula Milk", "Nutritional baby formula", 31, 7, 480, 14),

            };

            foreach (var product in products)
            {
                var exists = await context.Products
                    .FirstOrDefaultAsync(p => p.Name == product.Name);

                if (exists == null)
                {
                    await context.Products.AddAsync(product);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}