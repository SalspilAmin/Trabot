using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Cart.Queries.Models;

namespace Tradify.Core.Features.Cart.Commands.Models
{
    public class AddToCartCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public int CartId { get; set; } 


        public ProductVariantRequestToCart ProductVariant {  get; set; }

        
    }
    public class ProductVariantRequestToCart
    {
        public int Id { get; set; }
       
        public int ProductId { get; set; }
        public int Quentity {  get; set; }
  
  
    }

}
