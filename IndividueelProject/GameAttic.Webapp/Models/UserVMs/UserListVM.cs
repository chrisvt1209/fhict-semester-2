namespace GameAttic.Webapp.Models
{
    public class UserListVM
    {
        public List<UserVM>? UserList { get; set; }

        public UserListVM()
        {
            UserList = new List<UserVM>();
        }
    }
}
