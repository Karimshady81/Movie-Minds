namespace MovieMinds.Models.Entites
{
    public class Movie
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public double TmbdRating { get; set; }
        public int RunTime { get; set; }
        public string Status { get; set; } //Released, In Production, etc?
        public decimal Revenue { get; set; }
        public decimal Budget { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Navigation properties
        //public ICollection<Review> Reviews { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
        //public ICollection<WatchList> WatchLists { get; set; }
    }
}
