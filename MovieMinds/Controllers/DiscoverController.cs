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
        public async Task<IActionResult> Discover(int page = 1, string sort = "popularity.desc", string year = "", string genre = "")
        {
            var response = await _movies.GetDiscoverAsync(page,sort,year,genre);
            if (response == null)
                return StatusCode(502);

            var vm = new DiscoverViewModel
            {
                Movies = response.Results.AsReadOnly(),
                Page = page,
                TotalResults = response.TotalResults,
                TotalPages = response.TotalPages,
                CurrentSort = sort,
                CurrentYear = year,
                CurrentGenre = genre
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query, int page = 1)
        {
            var response = await _movies.GetSearchAsync(query,page);

            if (response == null)
                return StatusCode(502);

            var filteredMovies = response.Results
                .Where(m => m.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var vm = new DiscoverViewModel
            {
                Movies = filteredMovies.AsReadOnly(),
                Page = 1,
                TotalResults = filteredMovies.Count,
                TotalPages = 1,
            };
            return View("Discover", vm);
        }
    }
}
