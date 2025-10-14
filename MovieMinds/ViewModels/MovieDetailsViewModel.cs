using MovieMinds.Models.DTO;

namespace MovieMinds.ViewModels
{
    public class MovieDetailsViewModel
    {
        public TmdbMovieDto Movie { get; set; } = default!;
        public IReadOnlyList<ReleaseDateCountryDto>? ReleaseDates { get; set; }
        public IReadOnlyList<CrewMemberDto>? Crew { get; set; }
        public IReadOnlyList<CastMemberDto>? Cast { get; set; }


        //Helpers
        public CrewMemberDto? Director => Crew?.FirstOrDefault(c => c.Job == "Director");

        //Fallbacks
        public string DisplayYear => DateTime.TryParse(Movie.ReleaseDate.ToString(), out var d) ? d.Year.ToString() : "-";

        public string DisplayRunTime => Movie.RunTime > 0 ? $"{Movie.RunTime} mins" : "TBD";

        public string DisplayDirector => Director != null ? Director.Name : "-";

        public string DisplayTagLine => string.IsNullOrWhiteSpace(Movie.TagLine) ? "No Tag line available" : Movie.TagLine;

        public string DisplayOverview => string.IsNullOrWhiteSpace(Movie.Overview) ? "No Overview available" : Movie.Overview;
    }
}
