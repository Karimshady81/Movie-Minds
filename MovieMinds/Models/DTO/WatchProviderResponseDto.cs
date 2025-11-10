namespace MovieMinds.Models.DTO
{
    public class WatchProviderResponseDto
    {
        public Dictionary<string, CountryProvidersDto> Results { get; set; } = new();
    }
}
