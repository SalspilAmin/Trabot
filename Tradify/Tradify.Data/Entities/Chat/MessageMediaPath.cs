using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Entities.Chat
{
    public class MessageMediaPath
    {

        public int Id { get; set; }
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        public string MediaPath { get; set; }

        public bool IsDeleted { get; set; } = false;
        public virtual Message? Message { get; set; }
    }
}
