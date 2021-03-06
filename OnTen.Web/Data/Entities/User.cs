using Microsoft.AspNetCore.Identity;
using OnTen.Common.Entities;
using OnTen.Common.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnTen.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The Maxlength is {0}")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The Maxlength is {0}")]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The Maxlength is {0}")]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public String ImageId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == string.Empty || ImageId == null
            //? $"https://localhost:44379/Images/noimage.png"
            //: $"https://localhost:44379/Users/{ImageId}";
        ? $"http://onten.linkonext.com/Images/noimage.png"
        : $"http://onten.linkonext.com/Users/{ImageId}";

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}
