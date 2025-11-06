using AutoMapper;
using GameAttic.Application;
using GameAttic.Domain;
using GameAttic.Webapp.Handlers;
using GameAttic.Webapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAttic.Webapp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly SessionHandler _sessionHandler;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userService = new UserService(userRepository, mapper);
            _mapper = mapper;
            _sessionHandler = new SessionHandler(this);
        }

        public IActionResult Index()
        {
            var loggedInUser = _sessionHandler.GetLoggedInUser();
            if (loggedInUser == null || !loggedInUser.IsAdmin)
            {
                return View("AdminError");
            }
            UserListVM userListVM = new UserListVM();
            foreach (User user in _userService.GetAllUsers())
            {
                userListVM.UserList!.Add(new UserVM()
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    IsAdmin = user.Role == Role.Admin
                });
            }
            return View(userListVM);
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterUserVM model)
        {
            IActionResult actionResult = BadRequest();
            if (ModelState.IsValid)
            {
                User user = new User(
                    Guid.NewGuid(),
                    model.Username,
                    model.Password,
                    model.Email,
                    model.DisplayName,
                    model.Role
                    );
                _userService.AddUser(user);

                actionResult = RedirectToAction("Login");
            }
            return actionResult;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUserVM model)
        {
            if (ModelState.IsValid)
            {
                Guid? id = _userService.Login(model.Username, model.Password);

                if (id != null)
                {
                    Guid guid = (Guid)id;
                    string name = _userService.GetUserById(guid).DisplayName;
                    bool isAdmin = _userService.IsAdmin(guid);

                    HttpContext.Session.SetString("UserId", id.ToString()!);
                    HttpContext.Session.SetString("DisplayName", name);
                    HttpContext.Session.SetInt32("IsAdmin", isAdmin ? 1 : 0);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("InvalidLogin", "Invalid login credentials.");
                }
            }
            return View(model);
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

