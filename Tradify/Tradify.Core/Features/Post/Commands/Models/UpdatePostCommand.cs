using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Post.Commands.Models
{
    public class UpdatePostCommand : IRequest<Response<int>>
    {
        public int PostId { get; set; }

        public string? Content { get; set; }

        public string? Caption { get; set; }

        public List<IFormFile>? NewMediaFiles { get; set; }

        public bool ReplaceMedia { get; set; }
    }
}
