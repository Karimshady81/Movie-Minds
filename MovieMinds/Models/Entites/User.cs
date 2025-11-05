using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieMinds.Models.Entites
{
    public class User : IdentityUser
    {
        //Essential Properties
        [StringLength(50)]
        public string? DisplayName { get; set; }

        public ICollection<UserMovie> UserMovie { get; set; } = new List<UserMovie>();
        // ↑ This is a navigation property - it creates a relationship in the database
        // It means "a user can have many UserMovie records"

        [StringLength(500)]
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        ////Location/Country
        [StringLength(100)]
        public string? Location { get; set; }
    }
}
