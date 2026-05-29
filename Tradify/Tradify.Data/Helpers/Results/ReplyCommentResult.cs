using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Results
{
    public class ReplyCommentResult
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int CommentId { get; set; }
        public int UserIdThatWriteAReplyOFComment { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
