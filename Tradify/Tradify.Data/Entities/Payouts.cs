using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{

    public class Payouts
    {

        public int Id { get; set; }
        public int SotreId { get; set; }
        
        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public DateTimeOffset ProcessedAt {  get; set; }= DateTimeOffset.Now;

        

    }
}
