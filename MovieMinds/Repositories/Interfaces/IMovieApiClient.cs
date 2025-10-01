using MovieMinds.Models.Entites;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IMovieApiClient
    {
        Task<IReadOnlyList<Movie>> GetDiscoverAsync(int page = 1);
        Task<IReadOnlyList<Movie>> GetNowPlayingAsync(int page = 1);
        Task<Movie> GetMovieByIdAsync (int id);
        Task<IReadOnlyList<CastMember>> GetMovieCastAsync(int id);
        Task<IReadOnlyList<CrewMember>> GetMovieCrewAsync(int id);
    }
}
