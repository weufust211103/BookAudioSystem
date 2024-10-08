using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.Repositories.IRepositories
{
    public interface IBookRepository
    {
        // Book operations
        
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int id);
        Task DeleteBookAsync(Book book);

        // Tag operations
        Task<Tag> GetTagByIdAsync(int tagId);
        Task AddTagAsync(Tag tag);
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task UpdateTagAsync(Tag tag);
        Task DeleteTagAsync(int tagId);
        Task<Tag> GetTagByNameAsync(string tagName);

        // BookTag operations
        Task AddBookTagAsync(BookTag bookTag);
        Task RemoveBookTagAsync(int bookId, int tagId);
    }

}
