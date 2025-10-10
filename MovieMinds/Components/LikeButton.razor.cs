using Microsoft.AspNetCore.Components;

namespace MovieMinds.Components;

public partial class LikeButton : ComponentBase
{
    [Parameter] public int MovieId { get; set; }
    [Parameter] public bool Initial { get; set; }   

    private bool isLiked;

    protected override void OnInitialized() => isLiked = Initial;

    private void Toggle()
    {
        isLiked = !isLiked;
        StateHasChanged();
    }
}
