using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class ReleaseDateCountryDto
    {
        [JsonPropertyName("iso_3166_1")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("release_dates")]
        public IEnumerable<ReleaseDatesInfoDto>? ReleaseDates { get; set; }
    }
}