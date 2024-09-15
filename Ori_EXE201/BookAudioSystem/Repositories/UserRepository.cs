using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects;
using BookAudioSystem.Repositories.IRepositories;

namespace BookAudioSystem.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly BookAudioDbContext _context;

        public UserRepository(BookAudioDbContext context)
        {
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username);
        }
    }
}
