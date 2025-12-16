using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{
    public  class Reviews
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int CustomerId { get; set; }

        public int UserId { get; set; }

        public RatingValue Rating { get; set; }

        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Products? Product { get; set; }

       

    }
}
