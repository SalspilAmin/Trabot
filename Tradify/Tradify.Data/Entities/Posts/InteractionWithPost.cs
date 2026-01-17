using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities.Posts
{
    public class InteractionWithPost
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public InteractionType InteractionBy { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public virtual User? User { get; set; }

        public virtual Post? Post { get; set; }
    }
}
