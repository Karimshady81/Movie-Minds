using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;

namespace MovieMinds.ViewModels
{
    public class MovieDetailsViewModel
    {
        public TmdbMovieDto Movie { get; set; } = default!;
        public IReadOnlyList<CrewMember>? Crew { get; set; }
        public IReadOnlyList<CastMember>? Cast { get; set; }

        //Helpers
        public CrewMember? Director => Crew?.FirstOrDefault(c => c.Job == "Director");

        //public CastMember? ActorOrder(int position) => Cast?.Skip(Math.Max(0, position - 1)).FirstOrDefault();
        //public string Rating => Movie.TmbdRating.ToString("0.0");

        //Fallbacks
        public string DisplayYear => DateTime.TryParse(Movie.ReleaseDate.ToString(), out var d) ? d.Year.ToString() : "-";

        public string DisplayRunTime => Movie.RunTime > 0 ? $"{Movie.RunTime} mins" : "TBD";

        public string DisplayDirector => Director != null ? Director.Name : "-";

        public string DisplayTagLine => string.IsNullOrWhiteSpace(Movie.TagLine) ? "No Tag line available" : Movie.TagLine;

        public string DisplayOverview => string.IsNullOrWhiteSpace(Movie.Overview) ? "No Overview available" : Movie.Overview;
    }
}
