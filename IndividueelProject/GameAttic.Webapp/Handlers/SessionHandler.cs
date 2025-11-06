using GameAttic.Webapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAttic.Webapp.Handlers
{
    public class SessionHandler
    {
        private readonly Controller _controller;

        public SessionHandler(Controller controller)
        {
            _controller = controller;
        }

        public UserVM? GetLoggedInUser()
        {
            try
            {
                string UserId = _controller.HttpContext.Session.GetString("UserId");
                Guid.TryParse(UserId, out Guid guid);

                string DisplayName = _controller.HttpContext.Session.GetString("DisplayName")!;

                int IsAdminINT = (int)_controller.HttpContext.Session.GetInt32("IsAdmin")!;
                bool IsAdmin;

                if (IsAdminINT == 1)
                {
                    IsAdmin = true;
                }
                else
                {
                    IsAdmin = false;
                }

                UserVM loggedInUser = new UserVM()
                {
                    Id = guid,
                    DisplayName = DisplayName,
                    IsAdmin = IsAdmin
                };
                return loggedInUser;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
