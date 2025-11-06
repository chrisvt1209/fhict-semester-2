using GameAttic.Application;

namespace GameAttic.Tests
{
    public class PlatformMockRepo : IPlatformRepository
    {
        public bool? ReturnValue { get; set; }
        public object MockData { get; internal set; }

        public bool AddPlatform(PlatformDto platform)
        {
            Assert.NotNull(ReturnValue);
            MockData = platform;

            return ReturnValue.Value;
        }

        public List<PlatformDto> GetAllPlatforms()
        {
            Assert.NotNull(MockData);
            return (List<PlatformDto>)MockData;
        }

        public PlatformDto? GetPlatformById(Guid id)
        {
            Assert.NotNull(MockData);
            return (PlatformDto)MockData;
        }

        public List<PlatformDto> GetPlatformsByGame(Guid gameId)
        {
            Assert.NotNull(MockData);
            return (List<PlatformDto>)MockData;
        }

        public bool EditPlatform(PlatformDto platform)
        {
            Assert.NotNull(ReturnValue);
            MockData = platform;

            return ReturnValue.Value;
        }

        public bool DeletePlatform(Guid id)
        {
            Assert.NotNull(ReturnValue);
            return ReturnValue.Value;
        }
    }
}
