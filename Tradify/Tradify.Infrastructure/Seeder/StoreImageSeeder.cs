using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Tradify.Infrastructure.Seeder
{
    public class StoreImageSeeder
    {
        private static StoreImage AddStoreImage(int storeId, string mediaPath, string publicId)
        {
            return new StoreImage
            {
                StoreId = storeId,
                MediaPath = mediaPath,
                PublicId = publicId

            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var storeImages = new List<StoreImage>()
            {

                      AddStoreImage(1, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777464443/Store/1/hbp14ecczhmivbvldfjw.png"
                      , "Store/1/hbp14ecczhmivbvldfjw"),

                      AddStoreImage (2, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777464453/Store/2/yrcexw8qernbgbmszwwv.png"
                      , "Store/2/yrcexw8qernbgbmszwwv"),

                      AddStoreImage(3, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777464476/Store/3/lnqlyuqelntvhubhjoz1.png"
                      , "Store/3/lnqlyuqelntvhubhjoz1"),

                      AddStoreImage(4, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777464497/Store/4/liluduljo51ynur5mbcd.png"
                      , "Store/4/liluduljo51ynur5mbcd"),

                      AddStoreImage(5, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777464509/Store/5/l4uy6fnwxwbuo507tqqq.png"
                      , "Store/5/l4uy6fnwxwbuo507tqqq"),

                      AddStoreImage(6, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777464648/Store/6/blo7nyr8u9qng8jplqg6.png"
                      , "Store/6/blo7nyr8u9qng8jplqg6"),

                      AddStoreImage(7, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777464665/Store/7/yyp6wppighj6uosszxbc.png"
                      , "Store/7/yyp6wppighj6uosszxbc"),

                      AddStoreImage(8, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777491818/Store/8/rpfqombg2k3ylktzn7uv.png"
                      , "Store/8/rpfqombg2k3ylktzn7uv"),

                      AddStoreImage(9, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777491996/Store/9/klvtz1gs0i8jotgqzwfe.png"
                      , "Store/9/klvtz1gs0i8jotgqzwfe"),

                      AddStoreImage(10, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777492101/Store/10/bhjhseqrh9ztsbsbxlve.png"
                      , "Store/10/bhjhseqrh9ztsbsbxlve"),

                      AddStoreImage(11, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777492181/Store/11/pxkhu9nzfvvvg8izveta.png"
                      , "Store/11/pxkhu9nzfvvvg8izveta"),

                      AddStoreImage(12, "https://res.cloudinary.com/dftqokq6j/image/upload/v1777492269/Store/12/pvuwbholcye3mnp41sd7.png"
                      , "Store/12/pvuwbholcye3mnp41sd7")
            };


            foreach (var storeImage in storeImages)
            {
                var existingStoreImage = await context.StoreImages.FirstOrDefaultAsync(s => s.StoreId == storeImage.StoreId);

                if (existingStoreImage == null)
                {
                    await context.StoreImages.AddAsync(storeImage);
                    await context.SaveChangesAsync();

                }
            }

        }
    }
}
