using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;
using MovieMinds.Repositories.Interfaces;

namespace MovieMinds.Repositories
{
    public class TmdbMovieApiClient : IMovieApiClient
    {
        private readonly HttpClient _httpClient;

        public TmdbMovieApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MovieListResponeDto?> GetDiscoverAsync(int page = 1, string sort = "popularity.desc", string year = "", string genre = "")
        {
            var response = await _httpClient.GetAsync($"discover/movie?vote_average.gte=2&vote_count.gte=10&page={page}&sort_by={sort}&primary_release_year={year}&with_genres={genre}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<MovieListResponeDto>();
        }

        public async Task<MovieListResponeDto?> GetSearchAsync(string query, int page = 1)
        {
            var response = await _httpClient.GetAsync($"search/movie?query={query}&page={page}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<MovieListResponeDto>();
        }

        public async Task<MovieListResponeDto?> GetNowPlayingAsync(int page = 1)
        {
            var response = await _httpClient.GetAsync($"movie/now_playing?page={page}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<MovieListResponeDto>();
        }

        public async Task<TmdbMovieDto?> GetMovieByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movie/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TmdbMovieDto>();
        }

        public async Task<MovieReleaseDatesDto?> GetMovieReleaseDates(int id)
        {
            var respone = await _httpClient.GetAsync($"movie/{id}/release_dates");
            respone.EnsureSuccessStatusCode();

            return await respone.Content.ReadFromJsonAsync<MovieReleaseDatesDto>(); 
        }

        public async Task<IReadOnlyList<CastMemberDto>> GetMovieCastAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movie/{id}/credits");
            response.EnsureSuccessStatusCode();

            var castCreditsResponse = await response.Content.ReadFromJsonAsync<MovieCreditsDto>();
            return castCreditsResponse?.Cast ?? new List<CastMemberDto>();
        }

        public async Task<IReadOnlyList<CrewMemberDto>> GetMovieCrewAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movie/{id}/credits");
            response.EnsureSuccessStatusCode();

            var crewCreditsResponse = await response.Content.ReadFromJsonAsync<MovieCreditsDto>();
            return crewCreditsResponse?.Crew ?? new List<CrewMemberDto>();
        }

        public async Task<List<CountryNamesDto>?> GetCountriesName()
        {
            var response = await _httpClient.GetAsync("configuration/countries");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<CountryNamesDto>>();
        }
    }
}
