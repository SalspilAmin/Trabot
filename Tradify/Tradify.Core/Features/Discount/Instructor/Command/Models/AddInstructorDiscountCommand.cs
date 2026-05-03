using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Discount.Instructor.Command.Models
{
    public class AddInstructorDiscountCommand : IRequest<Response<string>>
    {
        public decimal Discount { get; set; }
    }
}
