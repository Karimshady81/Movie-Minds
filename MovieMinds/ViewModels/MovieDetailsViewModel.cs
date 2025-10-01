using MovieMinds.Models.Entites;

namespace MovieMinds.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; } = default!;
        public IReadOnlyList<CrewMember>? Crew { get; set; }
        public IReadOnlyList<CastMember>? Cast { get; set; }

        public CrewMember? Director => Crew?.FirstOrDefault(c => c.Job == "Director");
        
        //public CastMember? ActorOrder(int position) => Cast?.Skip(Math.Max(0, position - 1)).FirstOrDefault();
    }
}
