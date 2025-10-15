using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class ReleaseDatesInfoDto
    {
        [JsonPropertyName("certification")]
        public string? Certification { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }
    }
}