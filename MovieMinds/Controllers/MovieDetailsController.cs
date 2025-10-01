using Microsoft.AspNetCore.Mvc;
using MovieMinds.Repositories.Interfaces;
using MovieMinds.ViewModels;

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
            var cast = await _movies.GetMovieCastAsync(id);
            var crew = await _movies.GetMovieCrewAsync(id);

            var viewModel = new MovieDetailsViewModel
            {
                Movie = movie,
                Crew = crew,
                Cast = cast
            };

            return View(viewModel);
        }
    }
}
