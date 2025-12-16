using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Helpers
{
    [NotMapped]
    public class UserName
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
