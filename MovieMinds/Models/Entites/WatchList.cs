namespace MovieMinds.Models.Entites
{
    public class WatchList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsWatched { get; set; } = false;

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
