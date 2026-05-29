using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Results
{
    public class CommentResult
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int PostId { get; set; }
         public int? ReplayOnCommentNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; } 
    }
}
