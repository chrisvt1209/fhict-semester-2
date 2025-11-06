namespace GameAttic.Application
{
    public interface IPlatformRepository
    {
        bool AddPlatform(PlatformDto platform);
        List<PlatformDto> GetAllPlatforms();
        PlatformDto? GetPlatformById(Guid id);
        public List<PlatformDto> GetPlatformsByGame(Guid gameId);
        bool EditPlatform(PlatformDto platform);
        bool DeletePlatform(Guid id);
    }
}
