using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.Repositories.IRepositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
    }
}
