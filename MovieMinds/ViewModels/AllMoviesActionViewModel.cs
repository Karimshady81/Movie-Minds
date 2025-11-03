using MovieMinds.Models.Entites;

namespace MovieMinds.ViewModels
{
    public class AllMoviesActionViewModel
    {
        public User CurrentUser { get; set; } = default!;
        public List<Movie> LikedMovies { get; set; } = new();
        public List<Movie> InWatchList { get; set; } = new();
        public List<Movie> WatchedMovies { get; set; } = new();
        public List<Movie> Reviewed { get; set; } = new();
    }
}
