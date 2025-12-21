using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{
    public  class Payments 
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        public int StoreId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public User? User { get; set; }

        [ForeignKey(nameof(StoreId))]

        public Stores? Store { get; set; }  

        [ForeignKey(nameof(OrderId))]
        public Orders? Order {  get; set; }

    }
}
