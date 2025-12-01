using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{


    public class Shipments
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ShpmentTrackingId {  get; set; }

        public DateTime UpdatedAT { get; set; }
        
    }
}
