using MovieMinds.Models.Entites;
using System.ComponentModel.DataAnnotations;

namespace MovieMinds.ViewModels
{
    public class EditProfileViewModel
    {
        [StringLength(50)]
        public string? DisplayName { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}
