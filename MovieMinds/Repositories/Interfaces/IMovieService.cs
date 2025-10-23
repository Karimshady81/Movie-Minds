using MovieMinds.Models.DTO;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IMovieService
    {
        Task<MovieListResponeDto?> GetDiscoverAsync(int page = 1, string sort = "popularity.desc", string year = "", string genre = "");
        Task<MovieListResponeDto?> GetSearchAsync(string query, int page = 1);
        Task<MovieListResponeDto?> GetNowPlayingAsync();
        Task<TmdbMovieDto?> GetMovieDetailsAsync(int id);
        Task<MovieListResponeDto?> GetMovieRecommendationsAsync(int id);
        Task<MovieReleaseDatesDto?> GetMovieReleaseDatesAsync(int id);
        Task<List<CountryNamesDto>?> GetCountryNamesAsync();
        Task<IReadOnlyList<CastMemberDto>> GetMovieCastAsync(int id);
        Task<IReadOnlyList<CrewMemberDto>> GetMovieCrewAsync(int id);
    }
}
