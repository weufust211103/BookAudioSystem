using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects;
using BookAudioSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookAudioSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RentalBookDbContext _context;

        public UserRepository(RentalBookDbContext context)
        {
            _context = context;
        }
        public async Task<IList<string>> GetUserRolesAsync(int userId)
        {
            var userRoles = await _context.UsersRoles
                .Where(ur => ur.UserID == userId)
                .Select(ur => ur.Role.RoleName) // Assuming there is a `Role` entity linked
                .ToListAsync();

            return userRoles;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.UsersRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserRolesAsync(int userId)
        {
            var userRoles = await _context.UsersRoles.Where(ur => ur.UserID == userId).ToListAsync();
            _context.UsersRoles.RemoveRange(userRoles);
            await _context.SaveChangesAsync();
        }
    }
}
