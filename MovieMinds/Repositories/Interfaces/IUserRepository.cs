using MovieMinds.Models.Entites;

namespace MovieMinds.Repositories.Interfaces
{
    public interface IUserRepository
    {
        //Basic CRUD operations
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);

        //Profile specific methods

        //Search and filtering

        //Following system

        //Statistics and analytics

        //Validation helpers
        //Task<bool> ExistsAsync(int id);
        //Task<bool> IsUsernameAvailableAsync(string username);
        //Task<bool> IsEmailAvailableAsync(string email);
    }
}
