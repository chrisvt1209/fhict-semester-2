using AutoMapper;
using GameAttic.Domain;

namespace GameAttic.Application
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public bool AddGenre(Genre genre)
        {
            GenreDto genreDTO = _mapper.Map<GenreDto>(genre);
            bool addedGenre = _genreRepository.AddGenre(genreDTO);
            return addedGenre;
        }

        public List<Genre> GetAllGenres()
        {
            List<GenreDto> genreDTOs = _genreRepository.GetAllGenres();
            List<Genre> genres = new List<Genre>();
            foreach (GenreDto genreDTO in genreDTOs)
            {
                genres.Add(_mapper.Map<Genre>(genreDTO));
            }
            return genres;
        }

        public Genre GetGenreById(Guid id)
        {
            GenreDto genreDTO = _genreRepository.GetGenreById(id)!;
            Genre genre = _mapper.Map<Genre>(genreDTO);
            return genre;
        }

        public List<Genre> GetGenresByGame(Guid id)
        {
            List<Genre> genres = new List<Genre>();
            List<GenreDto> genreDTOs = _genreRepository.GetGenresByGame(id);

            foreach (GenreDto genreDTO in genreDTOs)
            {
                genres.Add(_mapper.Map<Genre>(genreDTO));
            }

            return genres;
        }

        public bool EditGenre(Genre genre)
        {
            GenreDto genreDTO = _mapper.Map<GenreDto>(genre);
            bool updatedGenre = _genreRepository.EditGenre(genreDTO);
            return updatedGenre;
        }

        public bool DeleteGenre(Guid id)
        {
            bool deletedGenre = _genreRepository.DeleteGenre(id);
            return deletedGenre;
        }
    }
}
