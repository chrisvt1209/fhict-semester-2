using System.ComponentModel.DataAnnotations;

namespace GameAttic.Webapp.Models
{
    public record GenreVM
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public required string Name { get; init; }
    }
}
