namespace MovieMinds.Models.DTO
{
    public class MovieCardVm
    {
        public int Id { get; init; }
        public string Title { get; init; } = "";
        public string PosterUrl { get; init; } = "";
        public string Year { get; init; } = "—";
        public string Genres { get; init; } = "";
        public string VoteAverage { get; init; } = "NR"; 
    }
}
