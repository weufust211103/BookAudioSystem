using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects.Models;

namespace BookAudioSystem.Services.IService
{
    public interface IBookService
    {
        // Book CRUD

        Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();
        Task<BookResponseDto> CreateBookAsync(BookModel model);
        Task<BookResponseDto> UpdateBookAsync(int id, BookModel model);
        Task<BookResponseDto> GetBookByIdAsync(int id);
        Task<bool> DeleteBookAsync(int id);

        // Tag CRUD
        Task<Tag> GetTagByIdAsync(int tagId);
        Task<Tag> CreateTagAsync(TagModel model);
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task UpdateTagAsync(Tag tag);
        Task DeleteTagAsync(int tagId);

        // BookTag management
        Task AddBookTagAsync(int bookId, int tagId);
        Task RemoveBookTagAsync(int bookId, int tagId);

        Task<int?> GetOwnerIdByBookIdAsync(int bookId);
    }

}
