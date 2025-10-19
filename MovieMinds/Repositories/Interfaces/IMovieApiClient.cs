using MovieMinds.Models.DTO;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IMovieApiClient
    {
        Task<MovieListResponeDto?> GetDiscoverAsync(int page = 1);
        Task<MovieListResponeDto?> GetNowPlayingAsync(int page = 1);
        Task<TmdbMovieDto?> GetMovieByIdAsync (int id);
        Task<MovieReleaseDatesDto?> GetMovieReleaseDates(int id);
        Task<List<CountryNamesDto>?> GetCountriesName();
        Task<IReadOnlyList<CastMemberDto>> GetMovieCastAsync(int id);
        Task<IReadOnlyList<CrewMemberDto>> GetMovieCrewAsync(int id);
    }
}
