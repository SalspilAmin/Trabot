using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Results
{
    public class ImageOrVideoPathResults
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public string Image_Or_VideoPath { get; set; }

        public bool IsUpdated { get; set; }
    }
}
