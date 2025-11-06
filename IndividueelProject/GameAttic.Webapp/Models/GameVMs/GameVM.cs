using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameAttic.Webapp.Models
{
    public record GameVM
    {
        [Key]
        public Guid Id { get; init; }

        [DisplayName("Title")]
        [Required]
        public required string Title { get; init; }

        [DisplayName("Platform")]
        public List<PlatformVM> Platforms { get; init; } = new List<PlatformVM>();

        [DisplayName("Release Date")]
        [Required]
        public DateOnly ReleaseDate { get; init; }

        [DisplayName("Genre")]
        public List<GenreVM> Genres { get; init; } = new List<GenreVM>();

        [DisplayName("Price")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; init; }

        public string? ImageUrl { get; init; }
    }
}
