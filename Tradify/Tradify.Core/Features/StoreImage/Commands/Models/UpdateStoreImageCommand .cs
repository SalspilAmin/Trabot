using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.StoreImage.Commands.Models
{
    public class UpdateStoreImageCommand : IRequest<Response<string>>
    {
        public int ImageId { get; set; }
        public bool IsMain { get; set; }
        public int SortOrder { get; set; } = 0;
    }
    
}
