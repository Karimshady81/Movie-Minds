using Microsoft.AspNetCore.Components;
using MovieMinds.Data;
using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;
using MovieMinds.Services.Interfaces;
using System.Security.Claims;

namespace MovieMinds.Components;

public partial class LikeButton : ComponentBase
{
    [Inject]
    public IUserMovieService UserMovieService { get; set; } = default!;
    [Inject]
    public IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

    [Parameter] public int MovieId { get; set; } // The TMDB movie ID
    [Parameter] public TmdbMovieDto? MovieData { get; set; } // The full movie data from TMDB

    private bool isLiked;

    protected override async Task OnInitializedAsync()
    {
        // When component loads, check if user already liked this movie
        var userId = HttpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!string.IsNullOrEmpty(userId))
        {
            var userMovie = await UserMovieService.GetUserMovieAsync(userId, MovieId);
            isLiked = userMovie?.Liked ?? false;
        }
    }

    private async Task Toggle()
    {
        var userId = HttpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(string.IsNullOrEmpty(userId))
        {
            // Handle unauthenticated user case (e.g., show a message or redirect to login)
            return;
        }

        try
        {
            // Pass the MovieData so service can save it if needed
            isLiked = await UserMovieService.ToggleLikeAsync(userId, MovieId, MovieData);
            StateHasChanged();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error toggling like: {ex.Message}");
        }
    }
}
