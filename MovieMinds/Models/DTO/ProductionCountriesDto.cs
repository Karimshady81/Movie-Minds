using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class ProductionCountriesDto
    {
        [JsonPropertyName("iso_3166_1")]
        public string? UsName { get; set; }

        [JsonPropertyName("Name")]
        public string? CountryName { get; set; }
    }
}