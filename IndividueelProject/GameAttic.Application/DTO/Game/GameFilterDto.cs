using GameAttic.Domain;

namespace GameAttic.Application
{
    public record GameFilterDto
    {
        public string? Title { get; init; }
        public Guid? PlatformId { get; init; }
        public Guid? GenreId { get; init; }
        public GameOrderColumn? OrderColumn { get; init; }
        public bool IsAsc { get; init; } = true;
    }
}
