using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Comments;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities.Posts
{
    public class Post
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Caption { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public PostType PostType { get; set; }

        // Properties  that help in RelationShips
        public List<Comment>? Comments { get; set; }
        public User? User { get; set; }
      
        public virtual ICollection<ImageOrVideoPath>? ImageOrVideo_Paths { get; set; }
       public virtual ICollection<InteractionWithPost>  interactionWithPosts { get; set; }
    }
}
