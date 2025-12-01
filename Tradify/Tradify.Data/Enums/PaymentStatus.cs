using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Enums
{
    public enum PaymentStatus : byte
    {
        Unpaid = 0, 
        Paid =1,
        Pending=2 ,
        Failed=3,
        Cancelled =4,
        Refunded =5,
    }
}
