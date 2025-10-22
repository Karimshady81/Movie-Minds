using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MovieMinds.Repositories.Interfaces;
using MovieMinds.ViewModels;

namespace MovieMinds.Controllers
{
    public class DiscoverController : Controller
    {
        private readonly IMovieService _movies;

        public DiscoverController(IMovieService movies)
        {
            _movies = movies;
        }

        [HttpGet("/Discover")]
        public async Task<IActionResult> Discover(int page = 1, string sort = "popularity.desc", string year = "")
        {
            var response = await _movies.GetDiscoverAsync(page,sort,year);
            if (response == null)
                return StatusCode(502);

            var vm = new DiscoverViewModel
            {
                Movies = response.Results.AsReadOnly(),
                Page = page,
                TotalResults = response.TotalResults,
                TotalPages = response.TotalPages,
                CurrentSort = sort,
                CurrentYear = year
            };

            return View(vm);
        }
    }
}
