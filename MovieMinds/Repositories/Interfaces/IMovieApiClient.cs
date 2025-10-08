using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IMovieApiClient
    {
        Task<IReadOnlyList<TmdbMovieDto>> GetDiscoverAsync(int page = 1);
        Task<IReadOnlyList<TmdbMovieDto>> GetNowPlayingAsync(int page = 1);
        Task<TmdbMovieDto?> GetMovieByIdAsync (int id);
        Task<IReadOnlyList<CastMember>> GetMovieCastAsync(int id);
        Task<IReadOnlyList<CrewMember>> GetMovieCrewAsync(int id);
    }
}
