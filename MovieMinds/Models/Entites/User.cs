using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieMinds.Models.Entites
{
    public class User : IdentityUser
    {
        //These are additional properties
        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();




        

        //public string? ProfilePictureUrl { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime? LastActiveAt { get; set; }


        ////Location/Country
        //[StringLength(100)]
        //public string? Location { get; set; }

    }
}
