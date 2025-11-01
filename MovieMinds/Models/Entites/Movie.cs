using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieMinds.Models.Entites
{
    public class Movie
    {
        // Use TMDB id as PK
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(300)] public string Title { get; set; } = "";
        public string? ReleaseDate { get; set; }
        [MaxLength(400)] public string? PosterPath { get; set; }
        [MaxLength(400)] public string? BackdropPath { get; set; }

        public double? TmdbRating { get; set; }
        public int? RunTime { get; set; }

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}

