namespace MovieMinds.Models.Entites
{
    public class UserMovie
    {
        public int UserId { get; set; }
        public int MovieId { get; set; } // TMDB id or FK to Movie table

        public bool InWatchlist { get; set; }
        public bool Liked { get; set; }
        public bool Watched { get; set; }
        public double? Rating5 { get; set; }   // null if not rated

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // To track when the entry was last updated

        public User User { get; set; } = default!;
        public Movie Movie { get; set; } = default!;
    }
}
