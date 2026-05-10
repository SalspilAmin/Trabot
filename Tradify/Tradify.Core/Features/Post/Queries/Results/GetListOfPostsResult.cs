using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Comments;
using Tradify.Data.Entities.Posts;
using Tradify.Data.Enums;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.Post.Queries.Results
{
    public class GetListOfPostsResult
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
        public int?   interactionWithPostsNumber { get; set; }
    }

   
  
   

}
