using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Tradify.Data.Entities
{




    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; } 

        public double Discount { get; set; }

        public double FinalPrice => Price - (Price * (Discount / 100));


        public int StoreId { get; set; }

        public int CategoryId { get; set; } 

        public int NumberOfProductInStock { get; set; }
        public bool InStock => NumberOfProductInStock > 0;  

       




    }
}
