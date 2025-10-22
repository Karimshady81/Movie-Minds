using MovieMinds.Models.DTO;
using MovieMinds.Repositories.Interfaces;

namespace MovieMinds.Repositories
{
    public class MovieService : IMovieService
    {
        private readonly IMovieApiClient _tmdb;

        public MovieService(IMovieApiClient movieApiClient)
        {
            _tmdb = movieApiClient;
        }

        public Task<MovieListResponeDto?> GetDiscoverAsync(int page = 1, string sort = "popularity.desc", string year = "", string genre = "")
        {
            return _tmdb.GetDiscoverAsync(page,sort,year,genre);
        }

        public Task<MovieListResponeDto?> GetSearchAsync(string query, int page = 1)
        {
            return _tmdb.GetSearchAsync(query,page);
        }

        public Task<MovieListResponeDto?> GetNowPlayingAsync()
        {
            return _tmdb.GetNowPlayingAsync();
        }

        public Task<TmdbMovieDto?> GetMovieDetailsAsync(int id)
        {
            return _tmdb.GetMovieByIdAsync(id);
        }

        public Task<IReadOnlyList<CrewMemberDto>> GetMovieCrewAsync(int id)
        {
            return _tmdb.GetMovieCrewAsync(id);
        }

        public Task<IReadOnlyList<CastMemberDto>> GetMovieCastAsync(int id)
        {
            return _tmdb.GetMovieCastAsync(id);
        }

        public Task<MovieReleaseDatesDto?> GetMovieReleaseDatesAsync(int id)
        {
            return _tmdb.GetMovieReleaseDates(id);
        }

        public Task<List<CountryNamesDto>?> GetCountryNamesAsync()
        {
            return _tmdb.GetCountriesName();
        }
    }
}
