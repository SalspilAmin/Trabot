using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{
    public class SubOrders
    {

        public int Id { get; set; } 

        public int OrderId { get; set; }

        public int StoreId { get; set; }


        public OrderStatus Status { get; set; } 
         

        public DateTime CreatedAt { get; set; }






    }
}
