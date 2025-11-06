namespace GameAttic.Application
{
    public record GameDto
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required DateOnly ReleaseDate { get; init; }
        public required decimal Price { get; init; }
        public string? ImageUrl { get; init; }
    }
}
