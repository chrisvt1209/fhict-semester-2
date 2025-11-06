namespace GameAttic.Webapp.Models
{
    public record UserVM
    {
        public Guid Id { get; init; }
        public string DisplayName { get; init; }
        public string Email { get; init; }
        public bool IsAdmin { get; init; }
    }
}
