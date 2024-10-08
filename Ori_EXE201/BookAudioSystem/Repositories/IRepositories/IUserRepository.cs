using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<Role> GetRoleByNameAsync(string roleName);
        Task AddUserAsync(User user);
        Task AddUserRoleAsync(UserRole userRole);
        Task RemoveUserRolesAsync(int userId);
        Task UpdateUserAsync(User user);
        Task<IList<string>> GetUserRolesAsync(int userId);
        Task<User> GetUserByIdAsync(int id);

    }
}
