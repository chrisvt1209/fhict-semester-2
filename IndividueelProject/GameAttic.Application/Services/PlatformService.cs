using AutoMapper;
using GameAttic.Domain;

namespace GameAttic.Application
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformService(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        public bool AddPlatform(Platform platform)
        {
            PlatformDto platformDTO = _mapper.Map<PlatformDto>(platform);
            bool addedPlatform = _platformRepository.AddPlatform(platformDTO);
            return addedPlatform;
        }

        public List<Platform> GetAllPlatforms()
        {
            List<PlatformDto> platformDTOs = _platformRepository.GetAllPlatforms();
            List<Platform> platforms = new List<Platform>();
            foreach (PlatformDto platformDTO in platformDTOs)
            {
                platforms.Add(_mapper.Map<Platform>(platformDTO));
            }
            return platforms;
        }

        public Platform GetPlatformById(Guid id)
        {
            PlatformDto platformDTO = _platformRepository.GetPlatformById(id)!;
            Platform platform = _mapper.Map<Platform>(platformDTO);
            return platform;
        }

        public List<Platform> GetPlatformsByGame(Guid id)
        {
            List<Platform> platforms = new List<Platform>();
            List<PlatformDto> platformDTOs = _platformRepository.GetPlatformsByGame(id);

            foreach (PlatformDto platformDTO in platformDTOs)
            {
                platforms.Add(_mapper.Map<Platform>(platformDTO));
            }

            return platforms;

        }

        public bool EditPlatform(Platform platform)
        {
            PlatformDto platformDTO = _mapper.Map<PlatformDto>(platform);
            bool updatedPlatform = _platformRepository.EditPlatform(platformDTO);
            return updatedPlatform;
        }

        public bool DeletePlatform(Guid id)
        {
            bool deletedPlatform = _platformRepository.DeletePlatform(id);
            return deletedPlatform;
        }
    }
}
