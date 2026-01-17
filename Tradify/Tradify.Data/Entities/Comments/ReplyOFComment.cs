using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Data.Entities.Comments
{
    public class ReplyOFComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [ForeignKey("UserThatWriteAComment")]
        public string UserIdThatWriteAComment { get; set; }
        [ForeignKey("UserThatWriteAReplyOFComment")]
        public string UserIdThatWriteAReplyOFComment { get; set; }
        public bool IsDeleted { get; set; } = false;
        // Navigation properties
        public virtual Comment? Comment { get; set; }
        public virtual User? UserThatWriteAComment { get; set; }
        public virtual User? UserThatWriteAReplyOFComment { get; set; }
    }
}
