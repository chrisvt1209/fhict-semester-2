using GameAttic.Domain;

namespace GameAttic.Webapp.Models.GameVMs
{
    public record GameFilterVM
    {
        public string? Title { get; init; }
        public Guid? PlatformId { get; init; }
        public Guid? GenreId { get; init; }
        public GameOrderColumn OrderColumn { get; init; } = GameOrderColumn.Title;
        public bool IsAsc { get; init; } = true;
    }
}
