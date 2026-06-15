using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Service.Services.Ai
{
    public class AiRequest
    {
        public string Birthdate { get; set; }

        public string Gender { get; set; } = "male";

        public string City { get; set; } = "Cairo";

        public string Country { get; set; } = "Egypt";

        public int Top_K { get; set; } = 15;
    }
}
