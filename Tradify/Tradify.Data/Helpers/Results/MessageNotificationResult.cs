using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Results
{
    public class MessageNotificationResult
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
