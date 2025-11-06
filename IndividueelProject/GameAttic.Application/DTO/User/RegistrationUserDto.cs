namespace GameAttic.Application
{
    public record RegistrationUserDto
    {
        public required Guid Id { get; init; }
        public required string Username { get; init; }
        public required string Password { get; init; }
        public required string Email { get; init; }
        public required string DisplayName { get; init; }
        public required int Role { get; init; }
    }
}
