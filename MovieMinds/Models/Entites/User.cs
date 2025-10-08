using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieMinds.Models.Entites
{
    public class User 
    {
        //Essential Properties
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public ICollection<UserMovie> UserMovie { get; set; } = new List<UserMovie>();


        //These are additional properties
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;

        //[StringLength(500)]
        //public string? Bio { get; set; }

        //public string? ProfilePictureUrl { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime? LastActiveAt { get; set; }


        ////Location/Country
        //[StringLength(100)]
        //public string? Location { get; set; }

    }
}
