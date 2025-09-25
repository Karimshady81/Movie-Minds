using MovieMinds.Models.Entites;

namespace MovieMinds.ViewModels
{
    public class UserProfileViewModel
    {
        public IEnumerable<User> Users { get; }
        public string? Message { get; set; }

        public UserProfileViewModel(IEnumerable<User> users,string? message)
        {
            Users = users;
            Message = message;
        }
    }
}
