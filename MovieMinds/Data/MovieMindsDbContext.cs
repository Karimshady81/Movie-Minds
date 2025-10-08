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
    }
}
