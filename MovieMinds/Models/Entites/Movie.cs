using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMinds.Models.Entites
{
    public class Movie
    {
        [Key] public int Id { get; set; }           // TMDB id as PK
        [MaxLength(300)] public string Title { get; set; } = "";
        public DateTime? ReleaseDate { get; set; }
        [MaxLength(400)] public string? PosterPath { get; set; }
        [MaxLength(400)] public string? BackdropPath { get; set; }
        public double? TmdbRating { get; set; }
        public int? RunTime { get; set; }
        public DateTime CachedAtUtc { get; set; } = DateTime.UtcNow;

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}
