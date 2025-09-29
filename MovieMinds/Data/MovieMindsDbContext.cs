using Microsoft.EntityFrameworkCore;
using MovieMinds.Models.Entites;

namespace MovieMinds.Data
{
    public class MovieMindsDbContext : DbContext
    {
        public MovieMindsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }    
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }
    }
}
