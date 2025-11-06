using System.ComponentModel.DataAnnotations;

namespace GameAttic.Webapp.Models
{
    public class LoginUserVM
    {
        [Required(ErrorMessage = "Please, enter a username.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Please, enter a password.")]
        public required string Password { get; set; }

    }
}
