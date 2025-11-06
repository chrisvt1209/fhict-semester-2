namespace GameAttic.Webapp.Models
{
    public class PlatformListVM
    {
        public List<PlatformVM>? PlatformList { get; set; }

        public PlatformListVM()
        {
            PlatformList = new List<PlatformVM>();
        }
    }
}
