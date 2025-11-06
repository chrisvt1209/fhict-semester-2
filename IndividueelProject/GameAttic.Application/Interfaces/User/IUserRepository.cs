namespace GameAttic.Application
{
    public interface IUserRepository
    {
        bool AddUser(RegistrationUserDto user);
        List<UserDto> GetAllUsers();
        UserDto? GetUserById(Guid id);
        bool EditUser(RegistrationUserDto user);
        bool DeleteUser(Guid id);
        int GetRole(Guid roleId);
        Guid? Login(string username, string password);
    }
}
