using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Discount.Instructor.Command.Models
{
    public class AddStoreServiceDiscountCommand : IRequest<Response<string>>
    {
        public decimal Discount { get; set; }
    }
}
