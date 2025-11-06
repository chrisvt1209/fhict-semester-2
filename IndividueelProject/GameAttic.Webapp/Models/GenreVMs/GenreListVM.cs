namespace GameAttic.Webapp.Models
{
    public class GenreListVM
    {
        public List<GenreVM>? GenreList { get; set; }

        public GenreListVM()
        {
            GenreList = new List<GenreVM>();
        }
    }
}
