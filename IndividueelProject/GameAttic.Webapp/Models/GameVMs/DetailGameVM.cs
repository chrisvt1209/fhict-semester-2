using System.ComponentModel;

namespace GameAttic.Webapp.Models
{
    public record DetailGameVM
    {
        public required Guid GameId { get; init; }

        [DisplayName("Platform")]
        public required Guid PlatformId { get; init; }

        [DisplayName("Genre")]
        public required Guid GenreId { get; init; }
        public required string Title { get; init; }
        public List<PlatformVM> AllPlatforms { get; init; } = new List<PlatformVM>();
        public DateOnly ReleaseDate { get; init; }
        public List<GenreVM> AllGenres { get; init; } = new List<GenreVM>();
        public required decimal Price { get; init; }
        public string? ImageUrl { get; init; }
    }
}
