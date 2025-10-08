using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IMovieService
    {
        Task<IReadOnlyList<TmdbMovieDto>> GetHomeMoviesAsync();
        Task<TmdbMovieDto> GetMovieDetailsAsync(int id);
        Task<IReadOnlyList<CastMember>> GetMovieCastAsync(int id);
        Task<IReadOnlyList<CrewMember>> GetMovieCrewAsync(int id);
    }
}
