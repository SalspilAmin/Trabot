using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Enums
{
   public enum ShipmentStatus : byte
    {
        Pending=0, Shipped=1,
        Delivered=2,
        Returned=3

    }
}
