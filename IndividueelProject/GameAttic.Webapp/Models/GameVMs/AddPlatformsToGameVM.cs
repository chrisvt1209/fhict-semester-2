namespace GameAttic.Webapp.Models
{
    public record AddPlatformsToGameVM
    {
        public Guid GameId { get; init; }
        public Guid SelectedPlatformId { get; init; }
        public List<PlatformVM> AvailablePlatforms { get; init; } = new List<PlatformVM>();
    }
}
