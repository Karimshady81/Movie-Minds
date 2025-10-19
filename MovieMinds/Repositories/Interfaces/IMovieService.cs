using MovieMinds.Models.DTO;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IMovieService
    {
        Task<MovieListResponeDto?> GetDiscoverAsync();
        Task<MovieListResponeDto?> GetNowPlayingAsync();
        Task<TmdbMovieDto?> GetMovieDetailsAsync(int id);
        Task<MovieReleaseDatesDto?> GetMovieReleaseDatesAsync(int id);
        Task<List<CountryNamesDto>?> GetCountryNamesAsync();
        Task<IReadOnlyList<CastMemberDto>> GetMovieCastAsync(int id);
        Task<IReadOnlyList<CrewMemberDto>> GetMovieCrewAsync(int id);
    }
}
