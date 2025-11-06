namespace GameAttic.Application
{
    public record CreateGameDto
    {
        public required Guid GameId { get; init; }
        public required Guid PlatformId { get; init; }
        public required Guid GenreId { get; init; }
        public required string Title { get; init; }
        public required DateOnly ReleaseDate { get; init; }
        public required decimal Price { get; init; }
        public string? ImageUrl { get; init; }
    }
}
