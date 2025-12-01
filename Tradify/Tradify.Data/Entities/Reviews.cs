using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{
    public  class Reviews
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int CustomerId { get; set; }

        public RatingValue Rating { get; set; }

        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

       

    }
}
