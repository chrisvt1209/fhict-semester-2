using GameAttic.Application;

namespace GameAttic.Tests
{
    public class UserMockRepo : IUserRepository
    {
        public bool? ReturnValue { get; set; }
        public object MockData { get; internal set; }

        public bool AddUser(RegistrationUserDto user)
        {
            Assert.NotNull(ReturnValue);
            MockData = user;

            return ReturnValue.Value;
        }

        public List<UserDto> GetAllUsers()
        {
            Assert.NotNull(MockData);
            return (List<UserDto>)MockData;
        }

        public UserDto? GetUserById(Guid id)
        {
            Assert.NotNull(MockData);
            return (UserDto)MockData;
        }

        public bool EditUser(RegistrationUserDto user)
        {
            Assert.NotNull(ReturnValue);
            MockData = user;

            return ReturnValue.Value;
        }

        public bool DeleteUser(Guid id)
        {
            Assert.NotNull(ReturnValue);
            return ReturnValue.Value;
        }

        public int GetRole(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public Guid? Login(string username, string password)
        {
            Assert.NotNull(MockData);
            var users = (List<LoginUserDto>)MockData;
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user != null ? user.Id : (Guid?)null;
        }
    }
}
