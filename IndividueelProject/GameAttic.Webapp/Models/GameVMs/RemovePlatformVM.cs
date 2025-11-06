namespace GameAttic.Webapp.Models
{
    public record RemovePlatformVM
    {
        public Guid GameId { get; init; }
        public Guid PlatformId { get; init; }
    }
}
