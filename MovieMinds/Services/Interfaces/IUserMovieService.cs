using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;

namespace MovieMinds.Services.Interfaces
{
    public interface IUserMovieService
    {
        Task<bool> ToggleLikeAsync(string userId, int movieId, TmdbMovieDto? movieData = null);
        Task<UserMovie?> GetUserMovieAsync(string userId, int movieId);
    }
}
