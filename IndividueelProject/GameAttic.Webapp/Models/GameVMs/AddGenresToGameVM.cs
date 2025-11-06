namespace GameAttic.Webapp.Models
{
    public record AddGenresToGameVM
    {
        public Guid GameId { get; init; }
        public Guid SelectedGenreId { get; init; }
        public List<GenreVM> AvailableGenres { get; init; } = new List<GenreVM>();
    }
}
