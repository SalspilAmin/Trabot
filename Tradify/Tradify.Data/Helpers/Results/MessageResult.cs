using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Helpers.Results
{
    public class MessageResult
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int SenderId { get; set; }

        public string SenderName { get; set; }

        public int ReceiverId { get; set; }

        public string ReceiverName { get; set; }

        public bool IsRead { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public MessageType MessageType { get; set; }

        public List<string> MediaPaths { get; set; }
    }
}
