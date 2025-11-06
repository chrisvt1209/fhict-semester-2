using AutoMapper;
using GameAttic.Application;
using GameAttic.Domain;
using GameAttic.Webapp.Handlers;
using GameAttic.Webapp.Models;
using GameAttic.Webapp.Models.GameVMs;
using Microsoft.AspNetCore.Mvc;

namespace GameAttic.Webapp.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IPlatformService _platformService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly SessionHandler _sessionHandler;

        public GamesController(IGameRepository gameRepository, IPlatformRepository platformRepository,
            IGenreRepository genreRepository, IMapper mapper)
        {
            _gameService = new GameService(gameRepository, mapper);
            _platformService = new PlatformService(platformRepository, mapper);
            _genreService = new GenreService(genreRepository, mapper);
            _mapper = mapper;
            _sessionHandler = new SessionHandler(this);
        }

        public IActionResult Index(GameFilterVM query)
        {
            GamesIndexVM gameListVM = new GamesIndexVM()
            {
                GameFilterVM = query,
                AllPlatforms = _platformService.GetAllPlatforms().Select(p => new PlatformVM()
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList(),
                AllGenres = _genreService.GetAllGenres().Select(g => new GenreVM()
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()
            };
            GameFilter gameFilter = new GameFilter()
            {
                Title = query.Title,
                PlatformId = query.PlatformId,
                GenreId = query.GenreId,
                OrderColumn = query.OrderColumn,
                IsAsc = query.IsAsc
            };
            foreach (Game game in _gameService.GetAllGames(gameFilter))
            {
                List<Platform> platforms = _platformService.GetPlatformsByGame(game.Id);
                List<PlatformVM> platformVMs = new List<PlatformVM>();

                foreach (Platform platform in platforms)
                {
                    platformVMs.Add(new PlatformVM()
                    {
                        Id = platform.Id,
                        Name = platform.Name
                    });
                }
                List<Genre> genres = _genreService.GetGenresByGame(game.Id);
                List<GenreVM> genreVMs = new List<GenreVM>();

                foreach (Genre genre in genres)
                {
                    genreVMs.Add(new GenreVM()
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    });
                }
                gameListVM.GameList!.Add(new GameVM()
                {
                    Id = game.Id,
                    Title = game.Title,
                    Platforms = platformVMs,
                    ReleaseDate = game.ReleaseDate,
                    Genres = genreVMs,
                    Price = game.Price,
                    ImageUrl = game.ImageUrl
                });
            }

            return View(gameListVM);
        }

        public IActionResult Details(Guid id)
        {
            IActionResult actionResult = NotFound();
            Game? game = _gameService.GetGameById(id);

            if (game != null)
            {
                GameVM gameVM = new()
                {
                    Id = game.Id,
                    Title = game.Title,
                    ReleaseDate = game.ReleaseDate,
                    Price = game.Price,
                    ImageUrl = game.ImageUrl
                };

                List<Platform> platforms = _platformService.GetPlatformsByGame(game.Id);
                foreach (Platform platform in platforms)
                {
                    gameVM.Platforms.Add(new PlatformVM()
                    {
                        Id = platform.Id,
                        Name = platform.Name
                    });
                }

                List<Genre> genres = _genreService.GetGenresByGame(game.Id);
                foreach (Genre genre in genres)
                {
                    gameVM.Genres.Add(new GenreVM()
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    });
                }
                return View(gameVM);
            }
            else
            {
                return actionResult;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameVM model = new CreateGameVM
            {
                AllPlatforms = _platformService.GetAllPlatforms().Select(p => new PlatformVM()
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList(),
                AllGenres = _genreService.GetAllGenres().Select(g => new GenreVM()
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGameVM model)
        {
            IActionResult actionResult = BadRequest();
            if (ModelState.IsValid)
            {
                Game game = new Game(
                    Guid.NewGuid(),
                    model.Title,
                    model.ReleaseDate,
                    model.Price,
                    model.ImageUrl
                    );
                _gameService.AddGame(game, model.PlatformId, model.GenreId);

                actionResult = RedirectToAction("Index");
            }
            return actionResult;
        }

        [HttpGet]
        public IActionResult AddPlatformsToGame(Guid gameId)
        {
            IEnumerable<Guid> gamePlatforms = _platformService.GetPlatformsByGame(gameId).Select(p => p.Id);

            List<PlatformVM> availablePlatforms = _platformService.GetAllPlatforms()
                .Where(p => !gamePlatforms.Contains(p.Id))
                .Select(p => new PlatformVM()
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();

            if (!availablePlatforms.Any())
            {
                ModelState.AddModelError("", "All platforms are already added to the game.");
                return RedirectToAction("Details", new { id = gameId });
            }

            AddPlatformsToGameVM model = new AddPlatformsToGameVM
            {
                GameId = gameId,
                AvailablePlatforms = availablePlatforms
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPlatformsToGame(AddPlatformsToGameVM model)
        {
            if (ModelState.IsValid)
            {
                _gameService.AddPlatformToGame(model.GameId, model.SelectedPlatformId);
                return RedirectToAction("Details", new { id = model.GameId });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddGenresToGame(Guid gameId)
        {
            IEnumerable<Guid> gameGenres = _genreService.GetGenresByGame(gameId).Select(g => g.Id);

            List<GenreVM> availableGenres = _genreService.GetAllGenres()
                .Where(g => !gameGenres.Contains(g.Id))
                .Select(g => new GenreVM()
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList();

            if (!availableGenres.Any())
            {
                ModelState.AddModelError("", "All genres are already added to the game.");
                return RedirectToAction("Details", new { id = gameId });
            }

            AddGenresToGameVM model = new AddGenresToGameVM
            {
                GameId = gameId,
                AvailableGenres = availableGenres
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGenresToGame(AddGenresToGameVM model)
        {
            if (ModelState.IsValid)
            {
                _gameService.AddGenreToGame(model.GameId, model.SelectedGenreId);
                return RedirectToAction("Details", new { id = model.GameId });
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemovePlatformFromGame(Guid gameId, Guid platformId)
        {
            _gameService.RemovePlatformFromGame(gameId, platformId);
            return RedirectToAction("Details", new { id = gameId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveGenreFromGame(Guid gameId, Guid genreId)
        {
            _gameService.RemoveGenreFromGame(gameId, genreId);
            return RedirectToAction("Details", new { id = gameId });
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            IActionResult actionResult = NotFound();
            Game? game = _gameService.GetGameById(id);

            if (game != null)
            {
                Platform platform = _platformService.GetPlatformsByGame(game.Id).FirstOrDefault()!;
                Genre genre = _genreService.GetGenresByGame(game.Id).FirstOrDefault()!;

                CreateGameVM gameVM = new()
                {
                    GameId = game.Id,
                    PlatformId = platform?.Id ?? Guid.Empty,
                    GenreId = genre?.Id ?? Guid.Empty,
                    Title = game.Title,
                    ReleaseDate = game.ReleaseDate,
                    Price = game.Price,
                    ImageUrl = game.ImageUrl,
                    AllPlatforms = _platformService.GetAllPlatforms().Select(p => new PlatformVM
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToList(),
                    AllGenres = _genreService.GetAllGenres().Select(g => new GenreVM
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList()
                };
                return View(gameVM);
            }
            else
            {
                return actionResult;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameVM model)
        {
            if (ModelState.IsValid)
            {
                Game? existingGame = _gameService.GetGameById(model.Id);
                if (existingGame != null)
                {
                    existingGame.Title = model.Title;
                    existingGame.ReleaseDate = model.ReleaseDate;
                    existingGame.Price = model.Price;
                    existingGame.ImageUrl = model.ImageUrl;
                    _gameService.EditGame(existingGame);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Game not found");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _gameService.DeleteGame(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

