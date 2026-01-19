using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Wrappers
{
    public static class QueryableExtensions
    {
       public static async Task<PaginatedResult<T>> ToPaginationlist<T>(this IQueryable<T> source,int pagenumber,int pagesize)
            where T : class
        {

            if (source == null)
            {
                throw new Exception("Empty");
            }

            pagenumber = pagenumber <= 0 ? 1 : pagenumber;
            pagesize =  pagesize <= 0 ? 10 : pagesize;
             
            int count = await source.AsNoTracking().CountAsync();
            if(count == 0)  return  PaginatedResult<T>.Success(new List<T> (),count,pagenumber,pagesize);
            var items =await source.Skip((pagenumber-1)*pagesize).Take(pagesize).ToListAsync();
            return PaginatedResult<T>.Success(items,count,pagenumber,pagesize);
        }

    }
}
