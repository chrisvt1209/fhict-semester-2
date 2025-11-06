using GameAttic.Domain;

namespace GameAttic.Application
{
    public interface IPlatformService
    {
        bool AddPlatform(Platform platform);
        List<Platform> GetAllPlatforms();
        Platform GetPlatformById(Guid id);
        public List<Platform> GetPlatformsByGame(Guid id);
        bool EditPlatform(Platform platform);
        bool DeletePlatform(Guid id);
    }
}
