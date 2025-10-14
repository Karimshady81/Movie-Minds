using MovieMinds.Models.DTO;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IMovieService
    {
        Task<IReadOnlyList<TmdbMovieDto>> GetHomeMoviesAsync();
        Task<TmdbMovieDto?> GetMovieDetailsAsync(int id);
        Task<IReadOnlyList<ReleaseDateCountryDto>> GetMovieReleaseDatesAsync(int id);
        Task<IReadOnlyList<CastMemberDto>> GetMovieCastAsync(int id);
        Task<IReadOnlyList<CrewMemberDto>> GetMovieCrewAsync(int id);
    }
}
