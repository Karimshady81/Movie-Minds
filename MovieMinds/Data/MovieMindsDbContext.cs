using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieMinds.Models.Entites;

namespace MovieMinds.Data
{
    public class MovieMindsDbContext : IdentityDbContext<User>
    {
        public MovieMindsDbContext(DbContextOptions<MovieMindsDbContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }    
        public DbSet<UserMovie> UserMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMovie>(entity =>
            {
                // Composite primary key
                entity.HasKey(x => new { x.UserId, x.MovieId });

                // FKs
                entity.HasOne(x => x.User)
                      .WithMany(u => u.UserMovies)          // use the actual nav name in User (rename if needed)
                      .HasForeignKey(x => x.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Movie)
                      .WithMany(m => m.UserMovies)          // matches Movie nav
                      .HasForeignKey(x => x.MovieId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
