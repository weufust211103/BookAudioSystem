using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BookAudioSystem.Repositories
{
    public class BookRepository
    {
        private readonly BookAudioDbContext _context;

        public BookRepository(BookAudioDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.Include(b => b.Audios).ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Include(b => b.Audios)
                                 .FirstOrDefault(b => b.BookId == id);
        }
    }
}
