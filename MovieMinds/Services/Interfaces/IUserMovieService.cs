using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;

namespace MovieMinds.Services.Interfaces
{
    public interface IUserMovieService
    {
        Task<bool> ToggleUserMovieActionAsync(string userId,int movieId, UserMovieAction action, TmdbMovieDto? movieDto = null);
        Task<UserMovie?> GetUserMovieAsync(string userId, int movieId);
    }
}
