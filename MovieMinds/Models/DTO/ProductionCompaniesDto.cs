using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class ProductionCompaniesDto
    {
        //Production Companies
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("logo_path")]
        public string? LogoPath { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("origin_country")]
        public string? OriginCountry { get; set; }

    }
}