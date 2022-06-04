using Microsoft.AspNetCore.Http;
using OnTen.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnTen.Web.Models
{
    public class CategoryViewModel : Category
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
