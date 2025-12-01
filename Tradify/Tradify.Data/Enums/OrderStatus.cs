using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Enums
{
    public enum OrderStatus :byte
    {
        processing=0,
         shipped=1,
         delivered=2, 
        cancelled=3

    }
}
