namespace GameAttic.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public Role Role { get; set; }

        public User(Guid id)
        {
            Id = id;
        }

        public User(Guid id, string username, string password, string email, string displayName, Role role)
            : this(id)
        {
            Username = username;
            Password = password;
            Email = email;
            DisplayName = displayName;
            Role = role;
        }

        public User(Guid id, string email, string displayName, Role role)
            : this(id)
        {
            Email = email;
            DisplayName = displayName;
            Role = role;
        }
    }
}
