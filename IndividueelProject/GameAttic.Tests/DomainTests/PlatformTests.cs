using GameAttic.Domain;

namespace GameAttic.Tests.DomainTests
{
    public class PlatformTests
    {
        [Fact]
        public void PlatformConstructorHasSameData()
        {
            Guid platformId = Guid.NewGuid();
            string name = "Platform 1";

            Platform platform = new Platform(platformId, name);

            Assert.Equal(platformId, platform.Id);
            Assert.Equal(name, platform.Name);
        }
    }
}
