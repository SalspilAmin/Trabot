using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Entities.Posts;

namespace Tradify.Data.Entities.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        [MaxLength(5000)]
        public string Content { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsUpdated { get; set; } = false;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }

        // Properties  that help in RelationShips
        public virtual Post? Post { get; set; }
        public  User? User { get; set; }
       

        public virtual List<ReplyOFComment>? ReplyOFComments { get; set; }
    }
}
