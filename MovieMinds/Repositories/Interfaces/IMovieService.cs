using MovieMinds.Models.Entites;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IMovieService
    {
        Task<IReadOnlyList<Movie>> GetHomeMoviesAsync();
        Task<Movie> GetMovieDetailsAsync(int id);
    }
}
