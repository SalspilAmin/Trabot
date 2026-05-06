using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Tradify.Infrastructure.Seeder
{
    public class InstructorImageSeeder
    {
        private static InstructorImage AddInstructorImage(int instructorId, string mediaPath, string publicId)
        {
            return new InstructorImage
            {
                InstructorId = instructorId,
                MediaPath = mediaPath,
                PublicId = publicId

            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var instructorImages = new List<InstructorImage>()
            {

                      AddInstructorImage(1, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777581219/Instructor/1/vwb54uh6bm5uose1wffg.png"
                      , "Instructor/1/vwb54uh6bm5uose1wffg"),

                      AddInstructorImage (2, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777581444/Instructor/2/n7byclqwa5uumtgbepcb.png"
                      , "Instructor/2/n7byclqwa5uumtgbepcb"),


                      AddInstructorImage(3, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777581617/Instructor/3/zhco0m3pbjugvtztoj2f.png"
                      , "Instructor/3/zhco0m3pbjugvtztoj2f"),


                      AddInstructorImage(4, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777582103/Instructor/4/sh5hvogj81lqfhxjbhhx.png"
                      , "Instructor/4/sh5hvogj81lqfhxjbhhx"),


                      AddInstructorImage(5, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777582182/Instructor/5/j4pf61fevzuzqe7wvmr9.png"
                      , "Instructor/5/j4pf61fevzuzqe7wvmr9"),

                      AddInstructorImage(6, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777582337/Instructor/6/siljvujq33hpzl2w5ipu.png"
                      , "Instructor/6/siljvujq33hpzl2w5ipu"),

                      AddInstructorImage(7, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777582523/Instructor/7/tqtpno1w8k0putw6kghl.png"
                      , "Instructor/7/tqtpno1w8k0putw6kghl"),

                      AddInstructorImage(8, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777582654/Instructor/8/g6ohxjamgnthkgbldo20.png"
                      , "Instructor/8/g6ohxjamgnthkgbldo20"),

                      AddInstructorImage(9, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777582825/Instructor/9/n8ocjh3pyzicfbjnc6hl.png"
                      , "Instructor/9/n8ocjh3pyzicfbjnc6hl"),

                      AddInstructorImage(10, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777582945/Instructor/10/vxzejxrvkw2954go40r9.png"
                      , "Instructor/10/vxzejxrvkw2954go40r9")
            };


            foreach (var instructorImage in instructorImages)
            {
                var existinginstructorImage = await context.InstructorImage.FirstOrDefaultAsync(s => s.InstructorId == instructorImage.InstructorId);

                if (existinginstructorImage == null)
                {
                    await context.InstructorImage.AddAsync(instructorImage);
                    await context.SaveChangesAsync();

                }
            }
        }
    }
}
