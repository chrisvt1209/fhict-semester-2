using GameAttic.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameAttic.Webapp.Models
{
    public record RegisterUserVM
    {
        [Key]
        public Guid Id { get; init; }

        [DisplayName("Display Name")]
        [Required(ErrorMessage = "Display name is required.")]
        public required string DisplayName { get; init; }

        [Required(ErrorMessage = "Username is required.")]
        public required string Username { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public required string Password { get; init; }

        [DisplayName("Repeat password")]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string RepeatPassword { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        public required string Email { get; init; }

        [Required(ErrorMessage = "Choose a role")]
        public Role Role { get; init; }
    }
}
