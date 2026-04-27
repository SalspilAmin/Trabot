using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Enums
{
    public enum BookingStatus
    {
        Confirmed=1,  // اتوافق عليه واتحجز فعليًا
        Cancelled=2   // اتلغى (من العميل أو السيستم)
    }
}
