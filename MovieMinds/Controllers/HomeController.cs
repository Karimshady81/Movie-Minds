using Microsoft.AspNetCore.Mvc;
using MovieMinds.Repositories.Interfaces;
using System.Collections;

namespace MovieMinds.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movies;

        public HomeController(IMovieService movies)
        {
            _movies = movies;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _movies.GetHomeMoviesAsync());
        }
    }
}
