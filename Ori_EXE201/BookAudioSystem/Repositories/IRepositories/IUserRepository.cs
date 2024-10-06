using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<Role> GetRoleByNameAsync(string roleName);
        Task AddUserAsync(User user);
        Task AddUserRoleAsync(UserRole userRole);
        Task UpdateUserAsync(User user);

        Task<User> GetUserByIdAsync(int id);

    }
}
