namespace GameAttic.Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public Game(Guid id)
        {
            Id = id;
        }

        public Game(Guid id, string title, DateOnly releaseDate, decimal price, string? imageUrl)
            : this(id)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Price = price;
            ImageUrl = imageUrl;
        }
    }
}
