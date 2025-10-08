using Microsoft.EntityFrameworkCore;
using MovieMinds.Data;
using MovieMinds.Models.Entites;
using MovieMinds.Repositories.Interfaces;

namespace MovieMinds.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieMindsDbContext _movieMindsDbContext;

        public UserRepository(MovieMindsDbContext movieMindsDbContext)
        {
            _movieMindsDbContext = movieMindsDbContext;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            //you should inculde the other properties when they are available
            return await _movieMindsDbContext.Users
                                             .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _movieMindsDbContext.Users
                                       .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _movieMindsDbContext.Users
                                             .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _movieMindsDbContext.Users
                                             .OrderBy(u => u.DisplayName ?? u.UserName).ToListAsync();
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            _movieMindsDbContext.Users.Add(user);
            await _movieMindsDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            _movieMindsDbContext.Update(user);
            await _movieMindsDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _movieMindsDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _movieMindsDbContext.Remove(user);
            await _movieMindsDbContext.SaveChangesAsync();
            return true;
        }

        //public async Task<bool> ExistsAsync(int id)
        //{
        //    return await _movieMindsDbContext.Users.AnyAsync(u => u.Id == id);
        //}
        //public async Task<bool> IsUsernameAvailableAsync(string username)
        //{
        //    return !await _movieMindsDbContext.Users.AnyAsync(u => u.UserName == username);
        //}

        //public async Task<bool> IsEmailAvailableAsync(string email)
        //{
        //    return !await _movieMindsDbContext.Users.AnyAsync(u => u.Email == email);
        //}
    }
}
