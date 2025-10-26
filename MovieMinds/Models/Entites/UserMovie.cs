namespace MovieMinds.Models.Entites
{
    public class UserMovie
    {
        public string UserId { get; set; } = default!;
        public int MovieId { get; set; } 

        public bool InWatchlist { get; set; }
        public bool Liked { get; set; }
        public bool Watched { get; set; }
        public double? Rating5 { get; set; } 

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; } = default!;
        public Movie Movie { get; set; } = default!;
    }
}
