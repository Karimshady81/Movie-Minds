using MovieMinds.Models.Entites;

namespace MovieMinds.Models.DTO
{
    public class MovieCreditsResponse
    {
        public List<CastMember> Cast { get; set; } = new();
        public List<CrewMember> Crew { get; set; } = new();
    }
}
