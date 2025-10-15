using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class CountryNamesDto
    {
        [JsonPropertyName("iso_3166_1")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("english_name")]
        public string? EnglishName { get; set; }
    }
}