using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnTen.Common.Entities
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }

        public int ProductId { get; set; }

        //[JsonIgnore]
        //public Product Product { get; set; }


        [Display(Name = "Image")]
        public string ImageId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == string.Empty
        //? $"https://localhost:44379/Images/noimage.png"
        //: $"https://localhost:44379/Products/{ImageId}";

        ? $"http://onten.linkonext.com/Images/noimage.png"
        : $"http://onten.linkonext.com/Products/{ImageId}";
    }
}
