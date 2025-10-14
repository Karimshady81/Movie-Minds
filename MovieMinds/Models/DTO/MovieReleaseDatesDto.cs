using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO;

public class MovieReleaseDatesDto
{
    public IEnumerable<ReleaseDateCountryDto>? Results { get; set; }
}
