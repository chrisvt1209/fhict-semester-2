using AutoMapper;
using GameAttic.Application;
using GameAttic.Domain;

namespace GameAttic.Tests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly UserMockRepo _userMockRepo;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            _userMockRepo = new UserMockRepo();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new UserProfile())).CreateMapper();
            _userService = new UserService(_userMockRepo, _mapper);
        }

        [Fact]
        public void Test_AddUsers_Succeeds()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            User user = new User(id)
            {
                Username = "chris",
                Password = "password",
                Email = "testuser@email.nl",
                DisplayName = "Chris",
                Role = Role.Admin
            };
            _userMockRepo.ReturnValue = true;

            // Act
            bool result = _userService.AddUser(user);

            // Assert
            Assert.True(result);
            Assert.True(UserAndRegisterUserDTOAreEqual(user, (RegistrationUserDto)_userMockRepo.MockData));
        }

        [Fact]
        public void Test_AddUsers_Fails()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            User user = new User(id)
            {
                Username = "admin",
                Password = "adminpw",
                Email = "admin@gameattic.nl"
            };
            _userMockRepo.ReturnValue = false;

            // Act
            bool result = _userService.AddUser(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Test_GetAllUsers()
        {
            // Arrange
            _userMockRepo.MockData =
                new List<UserDto>
                {
                    new UserDto()
                    {
                        Id = Guid.NewGuid(),
                        DisplayName = "Test User",
                        Email = "email@testemail.org",
                        Role = 1
                    },

                    new UserDto()
                    {
                        Id = Guid.NewGuid(),
                        DisplayName = "Test User 2",
                        Email = "email2@testemail.org",
                        Role = 0
                    }

                };

            // Act
            List<User> users = _userService.GetAllUsers();

            // Assert
            Assert.NotNull(users);
            List<UserDto> userDTOs = (List<UserDto>)_userMockRepo.MockData;
            Assert.Equal(userDTOs.Count, users.Count);
            for (int i = 0; i < userDTOs.Count; i++)
            {
                Assert.True(UserAndUserDTOAreEqual(users[i], userDTOs[i]));
            }
        }

        [Fact]
        public void Test_GetUserById()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _userMockRepo.MockData = new UserDto
            {
                Id = id,
                DisplayName = "Test User 2",
                Email = "email2@testemail.org",
                Role = 0
            };

            // Act
            User retrievedUser = _userService.GetUserById(id);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.True(UserAndUserDTOAreEqual(retrievedUser, (UserDto)_userMockRepo.MockData));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_UpdateUser(bool expectedResult)
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            User updatedUser = new User(
                userId,
                "Anonymous",
                "Uy47281n_wdqb",
                "anonymous@gameattic.org",
                "Sample name",
                0
                );
            _userMockRepo.ReturnValue = expectedResult;

            // Act
            bool updateResult = _userService.EditUser(updatedUser);

            // Assert
            Assert.Equal(expectedResult, updateResult);
            Assert.True(UserAndRegisterUserDTOAreEqual(updatedUser, (RegistrationUserDto)_userMockRepo.MockData));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_DeleteGame(bool expectedResult)
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            _userMockRepo.ReturnValue = expectedResult;

            // Act
            bool deleteResult = _userService.DeleteUser(userId);

            // Assert
            Assert.Equal(expectedResult, deleteResult);
        }

        private bool UserAndRegisterUserDTOAreEqual(User user, RegistrationUserDto userDTO)
        {
            return 
                user.Id == userDTO.Id &&
                user.Username == userDTO.Username &&
                user.Password == userDTO.Password &&
                user.Email == userDTO.Email
                ;
        }

        private bool UserAndUserDTOAreEqual(User user, UserDto userDTO)
        {
            return 
                user.Id == userDTO.Id &&
                user.DisplayName == userDTO.DisplayName &&
                user.Email == userDTO.Email &&
                user.Role == (Role)userDTO.Role
                ;
        }
    }
}
