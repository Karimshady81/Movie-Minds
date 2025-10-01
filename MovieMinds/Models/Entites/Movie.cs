using System.Text.Json.Serialization;

namespace MovieMinds.Models.Entites
{
    public class Movie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = default!;

        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = default!;

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; } = default!;

        [JsonPropertyName("vote_average")]
        public double TmbdRating { get; set; }

        [JsonPropertyName("runtime")]
        public int RunTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = default!;

        [JsonPropertyName("tagline")]
        public string TagLine { get; set; } = default!;

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }

        [JsonPropertyName("budget")]
        public decimal Budget { get; set; }

        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }


        //Navigation properties
        //public ICollection<Review> Reviews { get; set; }
        //public ICollection<MovieGenre>? MovieGenres { get; set; }
        //public ICollection<WatchList> WatchLists { get; set; }
    }
}
