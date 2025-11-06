namespace GameAttic.Domain
{
    public class Platform
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Platform(Guid id)
        {
            Id = id;
        }
        public Platform(Guid id, string name)
            : this(id)
        {
            Name = name;
        }
    }
}
