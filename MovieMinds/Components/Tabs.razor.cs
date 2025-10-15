using Microsoft.AspNetCore.Components;
using MovieMinds.Models.DTO;

namespace MovieMinds.Components
{
    public partial class Tabs : ComponentBase
    {
        private string activeTab = "Cast";

        [Parameter] public List<CountryNamesDto?> CountryNames { get; set; } = default!;
        [Parameter] public TmdbMovieDto MovieDetails { get; set; } = default!;
        [Parameter] public MovieReleaseDatesDto MovieDates { get; set; } = default!;
        [Parameter] public IReadOnlyList<CrewMemberDto>? Crew { get; set; }
        [Parameter] public IReadOnlyList<CastMemberDto>? Cast { get; set; }

        private void SetTab(string tab)
        {
            activeTab = tab;
        }

        private static readonly string[] DisplayJobs =
        [
            "Director", "Director of Photography", "Producer",
            "Writer", "Casting",
            "Sound Mixer", "Editor",
            "Production Design", "Costume Design","Gaffer",
        ];

        private string GetReleaseTypeName(int type)
        {
            return type switch
            {
                1 => "Premiere",
                2 => "Theatrical (limited)",
                3 => "Theatrical",
                4 => "Digital",
                5 => "Physical",
                6 => "TV",
                _ => "Unknown"
            };
        }

        private IEnumerable<IGrouping<string, CrewMemberDto>> crewGroups = Enumerable.Empty<IGrouping<string, CrewMemberDto>>();
        private IEnumerable<CastMemberDto> castMembers = Enumerable.Empty<CastMemberDto>();
        private IEnumerable<IGrouping<int, (ReleaseDatesInfoDto ReleaseInfo, string CountryCode)>>? releaseDatesByType;

        protected override void OnParametersSet()
        {
            var castName = Cast ?? Array.Empty<CastMemberDto>();
            castMembers = castName
                            .OrderBy(c => c.Order)
                            .Take(20)
                            .ToList();

            var crewJobs = Crew ?? Array.Empty<CrewMemberDto>();
            var jobSet = new HashSet<string>(DisplayJobs, StringComparer.OrdinalIgnoreCase);

            crewGroups = crewJobs
                         .Where(c => !string.IsNullOrWhiteSpace(c.Job) && jobSet.Contains(c.Job.Trim()))
                         .GroupBy(c => c.Job!.Trim(), StringComparer.OrdinalIgnoreCase)
                         .Select(g => new
                         {
                             Group = g,
                             Priority = Array.FindIndex(DisplayJobs, j => string.Equals(j, g.Key, StringComparison.OrdinalIgnoreCase))
                         })
                         .OrderBy(x => x.Priority)
                         .Select(x => x.Group)
                         .ToList();

            releaseDatesByType = MovieDates.Results!
                                        .SelectMany(country => (country.ReleaseDates ?? Enumerable.Empty<ReleaseDatesInfoDto>())
                                        .Select(rd => (ReleaseInfo: rd, Country: country.CountryCode ?? "Unknown")))
                                        .OrderBy(x => x.ReleaseInfo.ReleaseDate)
                                        .GroupBy(x => x.ReleaseInfo.Type)
                                        .OrderBy(g => g.Key)
                                        .ToList();
        }

        private MarkupString FormatCrewMembers(IGrouping<string, CrewMemberDto> group, int maxCount = 5)
        {
            var members = group
                .OrderBy(p => p.Order)
                .ThenBy(p => p.Name)
                .Take(maxCount)
                .Select(m => $"<span class=\"text-slug\">{m.Name}</span>")
                .ToList();

            return new MarkupString(string.Join("", members));
        }



    }
}