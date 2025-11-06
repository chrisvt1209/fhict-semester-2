using System.ComponentModel.DataAnnotations;

namespace GameAttic.Webapp.Models
{
    public record PlatformVM
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public required string Name { get; init; }
    }
}
