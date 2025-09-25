using MovieMinds.Data;
using MovieMinds.Models.Entites;

namespace MovieMinds.Models.SeedData
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            MovieMindsDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider
                                                            .GetRequiredService<MovieMindsDbContext>();

            if (!context.Users.Any())
            {
                context.AddRange
                (
                    new User { UserName = "John smith", Email = "JohnSmith@email.com", Password = "password123", FirstName = "John", LastName = "Smith" },
                    new User { UserName = "Jane Doe", Email = "JaneDoe@email.com", Password = "password456", FirstName = "Jane", LastName = "Doe"}
                );
            }

            context.SaveChanges();
        }
    }
}
