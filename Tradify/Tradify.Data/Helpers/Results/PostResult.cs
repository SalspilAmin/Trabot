using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Helpers.Results
{
    public class PostResult
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Caption { get; set; }

        public int? UserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public PostType PostType { get; set; }

        // Properties  that help in RelationShips
        public int? CommentsNumber { get; set; }


        public virtual ICollection<ImageOrVideoPathResults>? ImageOrVideo_Paths { get; set; }
        public int? interactionWithPostsNumber { get; set; }
    }
}
