using Microsoft.AspNetCore.Mvc;
using MovieMinds.Repositories.Interfaces;

namespace MovieMinds.Controllers
{
    public class MovieDetailsController : Controller
    {
        private readonly IMovieService _movies;

        public MovieDetailsController(IMovieService movies)
        {
            _movies = movies;
        }

        [HttpGet("movie/details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movies.GetMovieDetailsAsync(id);

            return View(movie);
        }
    }
}
