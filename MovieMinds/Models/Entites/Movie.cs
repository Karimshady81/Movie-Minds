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

        public string Title { get; set; } = "";
        public string? ReleaseDate { get; set; }
        public string? PosterPath { get; set; }
        public string? BackdropPath { get; set; }

        public double? TmdbRating { get; set; }
        public int? RunTime { get; set; }

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}

