﻿using OnTen.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnTen.Web.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [MaxLength(50, ErrorMessage = "The Maxlength is {0}")]
        [Required(ErrorMessage = "The {0} field is Required")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Is Starred")]
        public bool IsStarred { get; set; }

        public int CategoryId { get; set; }


        //[JsonIgnore]
        public Category Category { get; set; }


        public ICollection<ProductImage> ProductImages { get; set; }


        [Display(Name = "Product Images Number")]
        public int ProductImagesNumber => ProductImages == null ? 0 : ProductImages.Count;

        //TODO: Pending to put the correct paths
        [Display(Name = "Image")]
        public string ImageFullPath => ProductImages == null || ProductImages.Count == 0
            ? $"https://localhost:44361/Images/noimage.png"
            //? $"http://onven.linkonext.com/Images/noimage.png"
            : ProductImages.FirstOrDefault().ImageFullPath;

    }
}