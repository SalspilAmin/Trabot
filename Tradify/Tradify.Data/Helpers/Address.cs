using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers
{
    public class Address
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FullAddress { get; set; }

        public string  City { get; set; }   

        public string  PostalCode { get; set; }

    }
}
