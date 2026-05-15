using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Tradify.Infrastructure.Seeder
{
    public class CartSeeder
    {

        private static Cart AddCart(int userId)
        {
            return new Cart
            {
                UserId = userId,
                IsDeleted = false
            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var carts = new List<Cart>()
            {
                AddCart(1),
                AddCart(2),
                AddCart(3),
                AddCart(4),
                AddCart(5),
                AddCart(6),
                AddCart(7),
                AddCart(8),
                AddCart(9),
                AddCart(10),

                AddCart(11),
                AddCart(12),
                AddCart(13),
                AddCart(14),
                AddCart(15),
                AddCart(16),
                AddCart(17),
                AddCart(18),
                AddCart(19),
                AddCart(20),

                AddCart(21),
                AddCart(22),
                AddCart(23),
                AddCart(25),
                AddCart(26),
                AddCart(27),

                AddCart(32),
                AddCart(33),
                AddCart(34),
                AddCart(35),

                AddCart(41),
                AddCart(42),
                AddCart(43),
                AddCart(44),
                AddCart(45),

                AddCart(51),
                AddCart(55),
                AddCart(56),
                AddCart(57),
                AddCart(58),
                AddCart(59),
                AddCart(60),

                AddCart(61),
                AddCart(62),
                AddCart(63),
                AddCart(64),
                AddCart(65),
                AddCart(66),
                AddCart(67),
                AddCart(68),
                AddCart(69),
                AddCart(70),

                AddCart(71),
                AddCart(72),
                AddCart(73),
                AddCart(74),
                AddCart(75),
                AddCart(76),
                AddCart(77),
                AddCart(78),
                AddCart(79),
                AddCart(80),

                AddCart(81),
                AddCart(82),
                AddCart(83),
                AddCart(84),
                AddCart(85),
                AddCart(86),
                AddCart(87),
                AddCart(88),
                AddCart(89),
                AddCart(90),

                AddCart(91),
                AddCart(92),
                AddCart(93),
                AddCart(94),
                AddCart(95),
                AddCart(96),
                AddCart(97),
                AddCart(98),
                AddCart(99),
                AddCart(100),

                AddCart(101),
                AddCart(102),
                AddCart(103),
                AddCart(104),
                AddCart(105),
                AddCart(106),
                AddCart(107),
                AddCart(108),
                AddCart(109),
                AddCart(110),

                AddCart(111),
                AddCart(112),
                AddCart(113),
                AddCart(114),
                AddCart(115),
                AddCart(116),
                AddCart(117),
                AddCart(118),
                AddCart(119),
                AddCart(120),

                AddCart(121),
                AddCart(122),
            };

            foreach (var cart in carts)
            {
                var exists = await context.Carts
                    .FirstOrDefaultAsync(c => c.UserId == cart.UserId);

                if (exists == null)
                {
                    await context.Carts.AddAsync(cart);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
