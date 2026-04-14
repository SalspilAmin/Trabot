using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Entities
{
    public class StoreImage
    {
        public int Id { get; set; }
        public string MediaPath { get; set; }
     
        public int StoreId { get; set; }


        [ForeignKey(nameof(StoreId))]
        public virtual Stores Stores { get; set; }
    }
}

