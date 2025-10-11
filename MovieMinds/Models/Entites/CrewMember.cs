using System.Text.Json.Serialization;

namespace MovieMinds.Models.Entites
{
    public class CrewMember
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("job")]
        public string Job { get; set; } = default!;

        [JsonPropertyName("department")]
        public string Department { get; set; } = default!;

        [JsonPropertyName("order")]
        public int Order { get; set; }

        //public string ProfilePath { get; set; } = default!;
    }
}
