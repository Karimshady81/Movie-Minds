namespace MovieMinds.Models.DTO
{
    public class MovieCreditsDto
    {
        public List<CastMemberDto> Cast { get; set; } = new();
        public List<CrewMemberDto> Crew { get; set; } = new();
    }
}
