using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities.Chat
{
     public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        [ForeignKey("SenderUser")]
        public int SenderId { get; set; }
        [ForeignKey("ReceiverUser")]
        public int ReceiverId { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public MessageType MessageType { get; set; }
        public virtual User? SenderUser { get; set; }
        public virtual User? ReceiverUser { get; set; }
        public virtual ICollection<MessageMediaPath>? MessageMediaPaths { get; set; } = new List<MessageMediaPath>();

    }
}
