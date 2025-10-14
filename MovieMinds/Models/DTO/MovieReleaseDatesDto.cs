using System.Text.Json.Serialization;

namespace MovieMinds.Models.DTO;

public class MovieReleaseDatesDto
{
    public List<ReleaseDateCountryDto>? Results { get; set; }
}
