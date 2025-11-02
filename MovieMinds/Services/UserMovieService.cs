using Microsoft.EntityFrameworkCore;
using MovieMinds.Data;
using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;
using MovieMinds.Repositories.Interfaces;
using MovieMinds.Services.Interfaces;

namespace MovieMinds.Services
{
    public enum UserMovieAction
    {
        Liked,
        InWatchList,
        Watched
    }

    public class UserMovieService : IUserMovieService
    {
        private readonly IDbContextFactory<MovieMindsDbContext> _contextFactory;

        public UserMovieService(IDbContextFactory<MovieMindsDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
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
            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.UserMovies
                .AsNoTracking()
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);
        }

        public async Task<bool> ToggleUserMovieActionAsync(string userId, int movieId, UserMovieAction action, TmdbMovieDto? movieDto = null)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            await EnsureMovieExistsAsync(context,movieId, movieDto);

            var userMovie = await context.UserMovies.FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (userMovie == null)
            {
                userMovie = CreateNewUserMovie(userId, movieId, action);
                context.UserMovies.Add(userMovie);
            }
            else
            {
                ToggleAction(userMovie, action);
                context.UserMovies.Update(userMovie);
            }

            await context.SaveChangesAsync();

            return action switch
            {
                UserMovieAction.Liked => userMovie.Liked,
                UserMovieAction.InWatchList => userMovie.InWatchlist,
                UserMovieAction.Watched => userMovie.Watched,
                _ => throw new ArgumentOutOfRangeException(nameof(action), "Invalid action")
            };
        }

        private async Task EnsureMovieExistsAsync(MovieMindsDbContext context, int movieId, TmdbMovieDto? movieDto)
        {
            var movieExists = await context.Movies.AnyAsync(m => m.Id == movieId);

            if (!movieExists)
            {
                if (movieDto == null)
                {
                    throw new ArgumentException("Movie data must be provided to add a new movie.");
                }

                var movieEntity = MapDtoToEntity(movieDto);
                context.Movies.Add(movieEntity);
                await context.SaveChangesAsync();
            }
        }

        private UserMovie CreateNewUserMovie(string userId, int movieId, UserMovieAction action)
        {
            var userMovie = new UserMovie
            {
                UserId = userId,
                MovieId = movieId,
                Liked = false,
                InWatchlist = false,
                Watched = false
            };

            switch (action)
            {
                case UserMovieAction.Liked:
                    userMovie.Liked = true;
                    break;
                case UserMovieAction.InWatchList:
                    userMovie.InWatchlist = true;
                    break;
                case UserMovieAction.Watched:
                    userMovie.Watched = true;
                    break;
            }

            return userMovie;
        }

        private void ToggleAction(UserMovie userMovie, UserMovieAction action)
        {
            switch (action)
            {
                case UserMovieAction.Liked:
                    userMovie.Liked = !userMovie.Liked;
                    break;
                case UserMovieAction.InWatchList:
                    userMovie.InWatchlist = !userMovie.InWatchlist;
                    break;
                case UserMovieAction.Watched:
                    userMovie.Watched = !userMovie.Watched;
                    break;
            }
        }
    }
    }
