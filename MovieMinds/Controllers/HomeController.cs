using Microsoft.AspNetCore.Mvc;
using MovieMinds.Repositories.Interfaces;
using MovieMinds.ViewModels;
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
            var response = await _movies.GetNowPlayingAsync();
            if (response is null)
                return StatusCode(502);

            var vm = new DiscoverViewModel
            {
                Movies = response.Results.AsReadOnly()
            };

            return View(vm);
        }
    }
}
