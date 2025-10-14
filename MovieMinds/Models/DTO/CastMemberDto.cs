using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class CastMemberDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("character")]
        public string Character { get; set; } = default!;

        //[JsonPropertyName("profile_path")]
        //public string ProfilePath { get; set; } = default!;
    }
}
