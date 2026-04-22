using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Core.Features.Cart.Queries.Results
{
    public class GetCartByUserIdQueryResult
    {
        public int CartId { get; set; }
        public int UserId { get; set; }

        public List<CartProductResult>? ProductsInCart { get; set; }

    }

    public class CartProductResult {

        public int Id { get; set; }         
        public int ProductVariantId { get; set; }
        
        public virtual ProductVariants ProductVariant { get; set; }
        public int Quantity { get; set; }
    }


}
