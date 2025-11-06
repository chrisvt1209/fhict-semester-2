namespace GameAttic.Webapp.Models
{
    public record RemoveGenreVM
    {
        public Guid GameId { get; init; }
        public Guid GenreId { get; init; }
    }
}
