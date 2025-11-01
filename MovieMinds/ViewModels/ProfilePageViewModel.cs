using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieMinds.Data;
using MovieMinds.Models.Entites;

namespace MovieMinds.ViewModels;

public class ProfilePageViewModel : PageModel
{
    public User CurrentUser { get; set; } = default!;
    public List<Movie> LikedMovies { get; set; } = new();
    public List<Movie> InWatchList { get; set; } = new();
    public List<Movie> Reviewed { get; set; } = new();

}
