using GameAttic.Domain;

namespace GameAttic.Tests.DomainTests
{
    public class UserTests
    {
        [Fact]
        public void UserConstructorHasSameData()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string userName = "Username";
            string passWord = "OIuq833n2f";
            string email = "test@email.com";
            string displayName = "display name";
            Role role = Role.Admin;

            // Act
            User user = new User(userId, userName, passWord, email, displayName, role);

            // Assert
            Assert.Equal(userId, user.Id);
            Assert.Equal(userName, user.Username);
            Assert.Equal(passWord, user.Password);
            Assert.Equal(email, user.Email);
            Assert.Equal(displayName, user.DisplayName);
            Assert.Equal(role, user.Role);
        }
    }
}
