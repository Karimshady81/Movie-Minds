namespace MovieMinds.Models.DTO;

public class MovieListResponeDto
{
    public int Page { get; set; }
    public List<TmdbMovieDto> Results { get; set; } = new();
}
