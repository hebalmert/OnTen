using System.ComponentModel.DataAnnotations;

namespace OnTen.Web.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
