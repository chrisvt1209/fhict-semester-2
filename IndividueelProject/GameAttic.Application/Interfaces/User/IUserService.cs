using GameAttic.Domain;

namespace GameAttic.Application
{
    public interface IUserService
    {
        bool AddUser(User user);
        List<User> GetAllUsers();
        User GetUserById(Guid id);
        bool EditUser(User user);
        bool DeleteUser(Guid id);
        Guid? Login(string username, string password);
        public bool IsAdmin(Guid id);
    }
}
