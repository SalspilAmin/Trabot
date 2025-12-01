using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{
    public  class Payments 
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public string CustomerId { get; set; }

        public int StoreId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

    }
}
