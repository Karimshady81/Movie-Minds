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

        public async Task<IReadOnlyList<TmdbMovieDto>> GetDiscoverAsync(int page = 1)
        {
            var response = await _httpClient.GetAsync($"discover/movie?page={page}");
            response.EnsureSuccessStatusCode();

            var dto = await response.Content.ReadFromJsonAsync<MovieListResponeDto>();
            return (dto?.Results ?? new()).AsReadOnly();
        }

        public async Task<IReadOnlyList<TmdbMovieDto>> GetNowPlayingAsync(int page = 1)
        {
            var response = await _httpClient.GetAsync($"movie/now_playing?page={page}");
            response.EnsureSuccessStatusCode();

            var dto = await response.Content.ReadFromJsonAsync<MovieListResponeDto>();
            return (dto?.Results ?? new()).AsReadOnly(); 
        }

        public async Task<TmdbMovieDto?> GetMovieByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movie/{id}?language=en-US");
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
            var response = await _httpClient.GetAsync($"movie/{id}/credits?language=en-US");
            response.EnsureSuccessStatusCode();

            var castCreditsResponse = await response.Content.ReadFromJsonAsync<MovieCreditsDto>();
            return castCreditsResponse?.Cast ?? new List<CastMemberDto>();
        }

        public async Task<IReadOnlyList<CrewMemberDto>> GetMovieCrewAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movie/{id}/credits?language=en-US");
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
