using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO
{
    public class CountryProvidersDto
    {
        public List<ProviderDto> Rent { get; set; } = new();
        public List<ProviderDto> Buy { get; set; } = new();
        public List<ProviderDto> Flatrate { get; set; } = new();
    }
}