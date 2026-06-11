using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Helpers.Results
{

    public class InteractionWithPostResult
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int? PostId { get; set; }

        public InteractionType InteractionBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
