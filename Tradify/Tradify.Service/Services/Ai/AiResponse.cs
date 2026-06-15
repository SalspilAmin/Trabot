using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Service.Services.Ai
{
    public class AiResponse
    {
        public List<int> product_ids { get; set; } = [];

        public int count { get; set; }
    }
}
