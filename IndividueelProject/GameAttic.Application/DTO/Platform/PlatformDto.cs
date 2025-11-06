namespace GameAttic.Application
{
    public record PlatformDto
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
