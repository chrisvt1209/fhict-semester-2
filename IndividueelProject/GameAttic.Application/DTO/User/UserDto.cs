namespace GameAttic.Application
{
    public record UserDto
    {
        public required Guid Id { get; init; }
        public required string DisplayName { get; init; }
        public required string Email { get; init; }
        public required int Role { get; init; }
    }
}
