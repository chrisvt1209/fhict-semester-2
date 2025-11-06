namespace GameAttic.Domain
{
    public class GameFilter
    {
        public string? Title { get; set; }
        public Guid? PlatformId { get; set; }
        public Guid? GenreId { get; set; }
        public GameOrderColumn OrderColumn { get; set; }
        public bool IsAsc { get; set; }

        public GameFilter()
        {
            IsAsc = true;
            OrderColumn = GameOrderColumn.Title;
        }
    }
}
