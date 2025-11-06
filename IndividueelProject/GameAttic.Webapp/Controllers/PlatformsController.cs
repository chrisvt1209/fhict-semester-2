using AutoMapper;
using GameAttic.Application;
using GameAttic.Domain;
using GameAttic.Webapp.Handlers;
using GameAttic.Webapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAttic.Webapp.Controllers
{
    public class PlatformsController : Controller
    {
        private readonly IPlatformService _platformService;
        private readonly IMapper _mapper;
        private readonly SessionHandler _sessionHandler;

        public PlatformsController(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformService = new PlatformService(platformRepository, mapper);
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
            PlatformListVM platformListVM = new PlatformListVM();
            foreach (Platform platform in _platformService.GetAllPlatforms())
            {
                platformListVM.PlatformList!.Add(new PlatformVM()
                {
                    Id = platform.Id,
                    Name = platform.Name
                });
            }
            return View(platformListVM);
        }

        [HttpGet]
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
        public IActionResult Create(PlatformVM model)
        {
            var user = _sessionHandler.GetLoggedInUser();
            if (user == null || !user.IsAdmin)
            {
                return View("AdminError");
            }
            IActionResult actionResult = BadRequest();
            if (ModelState.IsValid)
            {
                Platform platform = new Platform(
                    Guid.NewGuid(),
                    model.Name
                    );
                _platformService.AddPlatform(platform);

                actionResult = RedirectToAction("Index");
            }
            return actionResult;
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var user = _sessionHandler.GetLoggedInUser();
            if (user == null || !user.IsAdmin)
            {
                return View("AdminError");
            }
            Platform? platform = _platformService.GetPlatformById(id);
            if (platform != null)
            {
                PlatformVM platformVM = new()
                {
                    Id = platform.Id,
                    Name = platform.Name
                };
                return View(platformVM);
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
                Platform platform = new Platform(
                    model.Id,
                    model.Name
                    );
                _platformService.EditPlatform(platform);
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
            _platformService.DeletePlatform(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
