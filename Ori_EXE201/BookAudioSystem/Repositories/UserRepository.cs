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
    }
}
