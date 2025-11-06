namespace GameAttic.Domain
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Genre(Guid id)
        {
            Id = id;
        }

        public Genre(Guid id, string name)
            : this(id)
        {
            Name = name;
        }
    }
}
