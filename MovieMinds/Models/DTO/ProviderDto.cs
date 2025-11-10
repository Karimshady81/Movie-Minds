using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class ProviderDto
    {
        [JsonPropertyName("provider_id")]
        public int ProviderId { get; set; }

        [JsonPropertyName("logo_path")]
        public string ProviderLogo { get; set; } = string.Empty;

        [JsonPropertyName("provider_name")]
        public string ProviderName { get; set; } = string.Empty;

        [JsonPropertyName("display_priority")]
        public int DisplayPriority { get; set; }
    }
}