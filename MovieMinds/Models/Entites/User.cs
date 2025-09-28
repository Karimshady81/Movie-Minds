using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieMinds.Models.Entites
{
    public class User 
    {
        //Essential Properties
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        //These are additional properties
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Display Name")]
        public string? DisplayName { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        public string? ProfilePictureUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastActiveAt { get; set; }


        //Location/Country
        [StringLength(100)]
        public string? Location { get; set; }

        //Navigation properties - Collection of related data
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<WatchList> Watchlists { get; set; }
        public ICollection<UserFollowing> Followers { get; set; }
        public ICollection<UserFollowing> Following { get; set; }
    }
}
