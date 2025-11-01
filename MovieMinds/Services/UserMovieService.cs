using Microsoft.EntityFrameworkCore;
using MovieMinds.Data;
using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;
using MovieMinds.Repositories.Interfaces;
using MovieMinds.Services.Interfaces;

namespace MovieMinds.Services
{
    public class UserMovieService : IUserMovieService
    {
        private readonly MovieMindsDbContext _context;

        public UserMovieService(MovieMindsDbContext context)
        {
            _context = context;
        }

        // Helper method: Convert TmdbMovieDto → Movie Entity
        private Movie MapDtoToEntity(TmdbMovieDto dto)
        {
            // Take the DTO (from TMDB API) and create a Movie entity (for database)
            return new Movie
            {
                Id = dto.Id,                    // Same TMDB ID
                Title = dto.Title!,
                ReleaseDate = dto.ReleaseDate,
                PosterPath = dto.PosterPath,
                BackdropPath = dto.BackdropPath,
                TmdbRating = dto.VoteAverage,
                RunTime = dto.RunTime
            };
        }

        public async Task<UserMovie?> GetUserMovieAsync(string userId, int movieId)
        {
            return await _context.UserMovies
                .AsNoTracking()
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);
        }

        public async Task<bool> ToggleLikeAsync(string userId, int movieId, TmdbMovieDto? movieDto = null)
        {
            // STEP A: Check if this movie exists in our database
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);

            if(!movieExists)
            {
                // Movie doesn't exist in our database!
                if (movieDto == null)
                {
                    // We can't save it because we don't have the data
                    throw new InvalidOperationException($"Movie with ID {movieId} must be saved to the database first.");
                }

                // STEP B: Convert the DTO to an Entity and save it
                var movie = MapDtoToEntity(movieDto);
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }

            // STEP C: Now we can safely create/update UserMovie
            var userMovie = await _context.UserMovies.FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (userMovie == null)
            {
                // User hasn't liked this movie before - create new record
                userMovie = new UserMovie
                {
                    UserId = userId,
                    MovieId = movieId,
                    Liked = true
                };
                _context.UserMovies.Add(userMovie);
            }
            else
            {
                // User already has a record - toggle the like status
                userMovie.Liked = !userMovie.Liked;
                _context.UserMovies.Update(userMovie);
            }

            await _context.SaveChangesAsync();
            return userMovie.Liked;
        }
    }
}
