using Microsoft.AspNetCore.Components;
using MovieMinds.Models.DTO;
using MovieMinds.Services;
using MovieMinds.Services.Interfaces;
using System.Security.Claims;

namespace MovieMinds.Components;

public partial class WatchListButton : ComponentBase
{
    [Inject]
    public IUserMovieService UserMovieService { get; set; } = default!;
    [Inject]
    public IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

    [Parameter] public int MovieId { get; set; }
    [Parameter] public TmdbMovieDto? MovieData { get; set; }

    private bool inWatchList;

    protected override async Task OnInitializedAsync()
    {
        // When component loads, check if user already liked this movie
        var userId = HttpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!string.IsNullOrEmpty(userId))
        {
            var userMovie = await UserMovieService.GetUserMovieAsync(userId, MovieId);
            inWatchList = userMovie?.InWatchlist ?? false;
        }
    }

    private async Task Toggle()
    {
        var userId = HttpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            // Handle unauthenticated user case (e.g., show a message or redirect to login)
            return;
        }

        try
        {
            // Pass the MovieData so service can save it if needed
            inWatchList = await UserMovieService.ToggleUserMovieActionAsync(userId, MovieId, UserMovieAction.InWatchList, MovieData);
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error toggling Watch-List: {ex.Message}");
        }
    }

}