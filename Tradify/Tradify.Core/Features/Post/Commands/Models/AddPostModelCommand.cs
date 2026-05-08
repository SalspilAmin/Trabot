using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Post.Commands.Models
{
    public class AddPostModelCommand : IRequest<Response<int?>>
    {
        public string? Content { get; set; }
        public string? Caption { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public PostType PostType { get; set; }
        public IList<IFormFile> MediaFilles { get; set; }

    }
}
