using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Results
{
    public class PostInteractionResult
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string InteractionType { get; set; }
    }
}
