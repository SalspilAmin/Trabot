
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.User.Commands.Models
{
    public class EditUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string? Address { get; set; }

        public string? Phone { get; set; }
        public string? Country { get; set; }
    }
}
