using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Entities
{
    public class Categories
    {
        
        public int Id { get; set; } 

        public string Name { get; set; }    

        public int? ParentCategoryId { get; set; }

       


    }
}
