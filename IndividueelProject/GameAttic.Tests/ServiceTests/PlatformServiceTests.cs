using AutoMapper;
using GameAttic.Application;
using GameAttic.Domain;

namespace GameAttic.Tests.ServiceTests
{
    public class PlatformServiceTests
    {
        private readonly PlatformMockRepo _platformMockRepo;
        private readonly IMapper _mapper;
        private readonly PlatformService _platformService;

        public PlatformServiceTests()
        {
            _platformMockRepo = new PlatformMockRepo();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<PlatformProfile>()).CreateMapper();
            _platformService = new PlatformService(_platformMockRepo, _mapper);
        }

        [Fact]
        public void Test_AddPlatform_Succeeds()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Platform platform = new Platform(id)
            {
                Id = id,
                Name = "TestPlatform 1"
            };
            _platformMockRepo.ReturnValue = true;

            // Act
            bool result = _platformService.AddPlatform(platform);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_AddPlatform_Fails()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Platform platform = new Platform(id)
            {
                Id = id,
                Name = "TestPlatform 2"
            };
            _platformMockRepo.ReturnValue = false;

            // Act
            bool result = _platformService.AddPlatform(platform);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Test_GetAllPlatforms()
        {
            // Arrange
            _platformMockRepo.MockData =
                new List<PlatformDto>
                {
                    new PlatformDto()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Platform 1",
                    },

                    new PlatformDto()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Platform 2",
                    }
                };

            // Act
            List<Platform> platforms = _platformService.GetAllPlatforms();

            // Assert
            Assert.NotNull(platforms);
            Assert.NotNull(platforms);
            List<PlatformDto> platformDTOs = (List<PlatformDto>)_platformMockRepo.MockData;
            Assert.Equal(platformDTOs.Count, platforms.Count);

            List<Platform> mappedPlatforms = _mapper.Map<List<Platform>>(platformDTOs);

            for (int i = 0; i < platformDTOs.Count; i++)
            {
                Assert.Equal(mappedPlatforms[i].Id, platforms[i].Id);
                Assert.Equal(mappedPlatforms[i].Name, platforms[i].Name);
            }
        }

        [Fact]
        public void Test_GetPlatformById()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _platformMockRepo.MockData = new PlatformDto
            {
                Id = id,
                Name = "Platform 1"
            };

            // Act
            Platform retrievedPlatform = _platformService.GetPlatformById(id);

            // Assert
            Assert.NotNull(retrievedPlatform);
            Platform mappedPlatform = _mapper.Map<Platform>((PlatformDto)_platformMockRepo.MockData);
            Assert.Equal(mappedPlatform.Id, retrievedPlatform.Id);
            Assert.Equal(mappedPlatform.Name, retrievedPlatform.Name);
        }

        [Fact]
        public void Test_GetPlatformsByGame()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            List<PlatformDto> expectedPlatformDTOs = new List<PlatformDto>
            {
                new PlatformDto { Id = Guid.NewGuid(), Name = "Platform 1" },
                new PlatformDto { Id = Guid.NewGuid(), Name = "Platform 2" }
            };
            _platformMockRepo.MockData = expectedPlatformDTOs;

            // Act
            List<Platform> actualPlatforms = _platformService.GetPlatformsByGame(gameId);

            // Assert
            Assert.NotNull(actualPlatforms);
            Assert.Equal(expectedPlatformDTOs.Count, actualPlatforms.Count);

            List<Platform> mappedGenres = _mapper.Map<List<Platform>>(expectedPlatformDTOs);

            for (int i = 0; i < expectedPlatformDTOs.Count; i++)
            {
                Assert.Equal(mappedGenres[i].Id, actualPlatforms[i].Id);
                Assert.Equal(mappedGenres[i].Name, actualPlatforms[i].Name);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_UpdatePlatform(bool expectedResult)
        {
            // Arrange
            Guid platformId = Guid.NewGuid();
            Platform updatedPlatform = new Platform(
                platformId,
                "Platform 3"
                );
            _platformMockRepo.ReturnValue = expectedResult;

            // Act
            bool updateResult = _platformService.EditPlatform(updatedPlatform);

            // Assert
            Assert.Equal(expectedResult, updateResult);
            Platform mappedPlatform = _mapper.Map<Platform>((PlatformDto)_platformMockRepo.MockData);
            Assert.Equal(mappedPlatform.Id, updatedPlatform.Id);
            Assert.Equal(mappedPlatform.Name, updatedPlatform.Name);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_DeletePlatform(bool expectedResult)
        {
            // Arrange
            Guid platformId = Guid.NewGuid();
            _platformMockRepo.ReturnValue = expectedResult;

            // Act
            bool deleteResult = _platformService.DeletePlatform(platformId);

            // Assert
            Assert.Equal(expectedResult, deleteResult);
        }
    }
}
