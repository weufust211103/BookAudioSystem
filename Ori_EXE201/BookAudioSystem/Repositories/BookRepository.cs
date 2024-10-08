using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using BookAudioSystem.Repositories.IRepositories;

namespace BookAudioSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RentalBookDbContext _context;

        public BookRepository(RentalBookDbContext context)
        {
            _context = context;
        }

        // Book CRUD operations
        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.Include(b => b.BookTags).ThenInclude(bt => bt.Tag)
                                       .FirstOrDefaultAsync(b => b.BookID == bookId);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.BookTags).ThenInclude(bt => bt.Tag)
                                       .ToListAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        

        public async Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        // Tag CRUD operations
        public async Task<Tag> GetTagByIdAsync(int tagId)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagID == tagId);
        }
        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
        }

       

        public async Task AddTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(int tagId)
        {
            var tag = await GetTagByIdAsync(tagId);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        // BookTag operations
        public async Task AddBookTagAsync(BookTag bookTag)
        {
            _context.BookTags.Add(bookTag);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookTagAsync(int bookId, int tagId)
        {
            var bookTag = await _context.BookTags.FirstOrDefaultAsync(bt => bt.BookID == bookId && bt.TagID == tagId);
            if (bookTag != null)
            {
                _context.BookTags.Remove(bookTag);
                await _context.SaveChangesAsync();
            }
        }
    }

}
