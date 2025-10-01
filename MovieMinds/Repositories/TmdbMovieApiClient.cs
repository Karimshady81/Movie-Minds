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

        public async Task<IReadOnlyList<Movie>> GetDiscoverAsync(int page = 1)
        {
            var response = await _httpClient.GetAsync($"discover/movie?page={page}");
            if(!response.IsSuccessStatusCode)
                throw new HttpRequestException($"TMDB {(int)response.StatusCode}: {await response.Content.ReadAsStringAsync()}");

            var movieWrapper = await response.Content.ReadFromJsonAsync<MovieRespone>();
            return (movieWrapper?.Results ?? new List<Movie>()).AsReadOnly();
        }

        public async Task<IReadOnlyList<Movie>> GetNowPlayingAsync(int page = 1)
        {
            var response = await _httpClient.GetAsync($"movie/now_playing?page={page}");
            if(!response.IsSuccessStatusCode)
                throw new HttpRequestException($"TMDB {(int)response.StatusCode}: {await response.Content.ReadAsStringAsync()}");

            var movieWrapper = await response.Content.ReadFromJsonAsync<MovieRespone>();
            return (movieWrapper?.Results ?? new List<Movie>()).AsReadOnly();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movie/{id}?language=en-US");
            response.EnsureSuccessStatusCode();

            var movieReponse = await response.Content.ReadFromJsonAsync<Movie>();
            return movieReponse!;
        }

        public async Task<IReadOnlyList<CastMember>> GetMovieCastAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movie/{id}/credits?language=en-US");
            response.EnsureSuccessStatusCode();

            var castCreditsResponse = await response.Content.ReadFromJsonAsync<MovieCreditsResponse>();
            return castCreditsResponse?.Cast ?? new List<CastMember>();
        }

        public async Task<IReadOnlyList<CrewMember>> GetMovieCrewAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movie/{id}/credits?language=en-US");
            response.EnsureSuccessStatusCode();

            var crewCreditsResponse = await response.Content.ReadFromJsonAsync<MovieCreditsResponse>();
            return crewCreditsResponse?.Crew ?? new List<CrewMember>();
        }
    }
}
