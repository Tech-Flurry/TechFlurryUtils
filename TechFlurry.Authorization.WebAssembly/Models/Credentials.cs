using System.ComponentModel.DataAnnotations;

namespace TechFlurry.Authorization.WebAssembly.Models
{
    public class Credentials
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
    }
}