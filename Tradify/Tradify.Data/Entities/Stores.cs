using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Tradify.Data.Entities
{

    public class Stores
    {

        public int Id { get; set; }

        public int SellerId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public bool CreatedAt { get; set; }

    }
}
