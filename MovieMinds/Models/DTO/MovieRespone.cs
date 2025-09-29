using MovieMinds.Models.Entites;

namespace MovieMinds.Models.DTO
{
    public class MovieRespone
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; } = new();
    }
}
