using MovieMinds.Models.Entites;
using MovieMinds.Repositories.Interfaces;

namespace MovieMinds.Repositories
{
    public class MovieService : IMovieService
    {
        private readonly IMovieApiClient _tmdb;

        public MovieService(IMovieApiClient movieApiClient)
        {
            _tmdb = movieApiClient;
        }


        public Task<IReadOnlyList<Movie>> GetHomeMoviesAsync()
        {
            return _tmdb.GetNowPlayingAsync();
        }

        public Task<Movie> GetMovieDetailsAsync(int id)
        {
            return _tmdb.GetMovieByIdAsync(id);
        }
    }
}
