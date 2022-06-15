using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTen.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnTen.Web.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


        //Para Manejar los precios y evitar problemas 
        //del usao de Punto o Coma como decimales.
        [Display(Name = "Price")]
        [MaxLength(12)]
        [RegularExpression(@"^\d+([\.\,]?\d+)?$", ErrorMessage = "Use only numbers and . or , to put decimals")]
        [Required]
        public string PriceString { get; set; }


        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
