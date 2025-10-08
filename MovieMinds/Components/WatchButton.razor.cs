using Microsoft.AspNetCore.Components;

namespace MovieMinds.Components;

public partial class WatchButton : ComponentBase
{
    [Parameter] public int MovieId { get; set; }
    [Parameter] public bool Initial { get; set; }   // from MVC model
    private bool isWatched;

    protected override void OnInitialized() => isWatched = Initial;

    private void Toggle()
    {
        isWatched = !isWatched;
        // TODO: call API later
        StateHasChanged();
    }
}
