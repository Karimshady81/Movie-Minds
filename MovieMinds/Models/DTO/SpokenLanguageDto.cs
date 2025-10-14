using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class SpokenLanguageDto
    {
        [JsonPropertyName("english_name")]
        public string? Language { get; set; }
    }
}