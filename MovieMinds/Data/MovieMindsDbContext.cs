 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieMinds.Models.Entites;

namespace MovieMinds.Data
{
    public class MovieMindsDbContext : IdentityDbContext<User>
    {
        public MovieMindsDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet properties - these become tables
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        // Users is inherited from IdentityDbContext<User>


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //← Identity configuration

            // UserMovie configuration
            modelBuilder.Entity<UserMovie>(entity =>
            {
                // Step 1: Define composite primary key
                entity.HasKey(um => new { um.UserId, um.MovieId });

                // Step 2: Configure User relationship
                entity.HasOne(um => um.User) //UserMovie has ONE User
                      .WithMany(u => u.UserMovie) //User has MANY UserMovies
                      .HasForeignKey(um => um.UserId) //FK is UserId
                      .OnDelete(DeleteBehavior.Cascade); //Delete UserMovie when User is deleted

                // Step 3: Configure Movie relationship
                entity.HasOne(um => um.Movie) // UserMovie has ONE Movie
                      .WithMany(m => m.UserMovies) // Movie has MANY UserMovies
                      .HasForeignKey(um => um.MovieId) // FK is MovieId
                      .OnDelete(DeleteBehavior.Cascade); // Delete UserMovies when Movie is deleted
            });
        }
    }
}
