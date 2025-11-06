namespace GameAttic.Webapp.Models
{
    public record CreateGameVM
    {
        public Guid GameId { get; init; }
        public Guid PlatformId { get; init; }
        public Guid GenreId { get; init; }
        public string Title { get; init; }
        public List<PlatformVM> AllPlatforms { get; init; } = new List<PlatformVM>();
        public DateOnly ReleaseDate { get; init; }
        public List<GenreVM> AllGenres { get; init; } = new List<GenreVM>();
        public decimal Price { get; init; }
        public string? ImageUrl { get; init; }
    }
}
