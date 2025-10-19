using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO;

public class MovieListResponeDto
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }

    [JsonPropertyName("results")]
    public List<TmdbMovieDto> Results { get; set; } = new();
}
