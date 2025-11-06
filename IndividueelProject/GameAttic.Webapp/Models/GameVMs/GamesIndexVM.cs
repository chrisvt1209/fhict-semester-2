using GameAttic.Webapp.Models.GameVMs;

namespace GameAttic.Webapp.Models
{
    public record GamesIndexVM
    {
        public List<GameVM>? GameList { get; init; } = new List<GameVM>();
        public List<PlatformVM> AllPlatforms { get; init; } = new List<PlatformVM>();
        public List<GenreVM> AllGenres { get; init; } = new List<GenreVM>();
        public GameFilterVM GameFilterVM { get; init; }

    }
}
