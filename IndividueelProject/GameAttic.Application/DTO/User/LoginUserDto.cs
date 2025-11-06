namespace GameAttic.Application
{
    public record LoginUserDto
    {
        public required Guid Id { get; init; }
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
