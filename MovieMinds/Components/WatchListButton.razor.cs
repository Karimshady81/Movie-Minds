using Microsoft.AspNetCore.Components;

namespace MovieMinds.Components;

public partial class WatchListButton : ComponentBase
{
    [Parameter] public int MovieId { get; set; }
    [Parameter] public bool Initial { get; set; }

    private bool isInWatchList;

    protected override void OnInitialized() => isInWatchList = Initial;

    private void Toggle()
    {
        isInWatchList = !isInWatchList;
        StateHasChanged();
    }

}