using Microsoft.AspNetCore.Components;
using MovieMinds.Models.Entites;

namespace MovieMinds.Components
{
    public partial class Tabs : ComponentBase
    {
        private string activeTab = "Cast";

        [Parameter] public IReadOnlyList<CrewMember>? Crew { get; set; }
        [Parameter] public IReadOnlyList<CastMember>? Cast { get; set; }

        private void SetTab(string tab)
        {
            activeTab = tab;
        }

        private IEnumerable<CastMember> GetCast()
        {
            return (Cast ?? Array.Empty<CastMember>())
                                  .OrderBy(c => c.Order)
                                  .Take(10);
        }

        private static readonly string[] DisplayOrder =
        [
            "Director", "Producers", "Producer", "Executive Producer",
            "Writer", "Original Writer", "Screenplay",
            "Casting", "Editor", "Cinematography", "Production Design",
            "Costume Design", "Music", "Sound", "Visual Effects"
        ];

        private IEnumerable<IGrouping<string, CrewMember>> crewGroups = Enumerable.Empty<IGrouping<string, CrewMember>>();

        protected override void OnParametersSet()
        {
            var items = Crew ?? Array.Empty<CrewMember>();

            crewGroups = items
                         .GroupBy(c => c.Job?.Trim() ?? "Other")
                         .OrderBy(g =>
                         {
                             var i = Array.IndexOf(DisplayOrder, g.Key);
                             return i < 0 ? int.MaxValue : i;
                         })
                         .ThenBy(g => g.Key);
        }
    }
}