using AutoMapper;
using GameAttic.Application;
using GameAttic.Domain;
using GameAttic.Webapp.Handlers;
using GameAttic.Webapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAttic.Webapp.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly SessionHandler _sessionHandler;

        public GenresController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreService = new GenreService(genreRepository, mapper);
            _mapper = mapper;
            _sessionHandler = new SessionHandler(this);
        }

        public IActionResult Index()
        {
            var user = _sessionHandler.GetLoggedInUser();
            if (user == null || !user.IsAdmin)
            {
                return View("AdminError");
            }
            GenreListVM genreListVM = new GenreListVM();
            foreach (Genre genre in _genreService.GetAllGenres())
            {
                genreListVM.GenreList!.Add(new GenreVM()
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            return View(genreListVM);
        }

        public IActionResult Create()
        {
            var user = _sessionHandler.GetLoggedInUser();
            if (user == null || !user.IsAdmin)
            {
                return View("AdminError");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreVM model)
        {
            var user = _sessionHandler.GetLoggedInUser();
            if (user == null || !user.IsAdmin)
            {
                return View("AdminError");
            }
            IActionResult actionResult = BadRequest();
            if (ModelState.IsValid)
            {
                Genre genre = new Genre(
                    Guid.NewGuid(),
                    model.Name
                    );
                _genreService.AddGenre(genre);

                actionResult = RedirectToAction("Index");
            }
            return actionResult;
        }

        public IActionResult Edit(Guid id)
        {
            var user = _sessionHandler.GetLoggedInUser();
            if (user == null || !user.IsAdmin)
            {
                return View("AdminError");
            }
            Genre? genre = _genreService.GetGenreById(id);
            if (genre != null)
            {
                GenreVM genreVM = new()
                {
                    Id = genre.Id,
                    Name = genre.Name
                };
                return View(genreVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlatformVM model)
        {
            var user = _sessionHandler.GetLoggedInUser();
            if (user == null || !user.IsAdmin)
            {
                return View("AdminError");
            }
            if (ModelState.IsValid)
            {
                Genre genre = new Genre(
                    model.Id,
                    model.Name
                    );
                _genreService.EditGenre(genre);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var user = _sessionHandler.GetLoggedInUser();
            if (user == null || !user.IsAdmin)
            {
                return View("AdminError");
            }
            _genreService.DeleteGenre(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
