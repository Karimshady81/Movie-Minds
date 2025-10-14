using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public sealed class TmdbMovieDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("title")] public string? Title { get; set; }
        [JsonPropertyName("overview")] public string? Overview { get; set; }
        [JsonPropertyName("release_date")] public DateTime? ReleaseDate { get; set; }
        [JsonPropertyName("poster_path")] public string? PosterPath { get; set; }
        [JsonPropertyName("backdrop_path")] public string? BackdropPath { get; set; }
        [JsonPropertyName("vote_average")] public double? VoteAverage { get; set; }
        [JsonPropertyName("runtime")] public int? RunTime { get; set; }
        [JsonPropertyName("status")] public string? Status { get; set; }
        [JsonPropertyName("tagline")] public string? TagLine { get; set; }


        //Details-tab properties
        [JsonPropertyName("revenue")] public long? Revenue { get; set; }
        [JsonPropertyName("budget")] public long? Budget { get; set; }
        [JsonPropertyName("production_companies")] public IEnumerable<ProductionCompaniesDto>? ProductionCompanies { get; set; }
        [JsonPropertyName("production_countries")] public IEnumerable<ProductionCountriesDto>? ProductionCountries { get; set; }
        [JsonPropertyName("spoken_languages")] public IEnumerable<SpokenLanguageDto>? SpokenLanguage { get; set; }
        [JsonPropertyName("genres")]public IEnumerable<GenresDto>? Genres { get; set; }
    }
}
