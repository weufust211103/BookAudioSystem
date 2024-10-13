using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Repositories.IRepositories;
using BookAudioSystem.Services.IService;

namespace BookAudioSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // Book CRUD operations
        public async Task<BookResponseDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return null;
            }

            return new BookResponseDto
            {
                BookID = book.BookID,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Image = book.Image,
                Price = book.Price,
                Status = book.Status,
                UserID = book.UserID
            };
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<BookResponseDto> CreateBookAsync(BookModel model)
        {
            var book = new Book
            {
                UserID = model.UserId,
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                Image = model.Image,
                Price = model.Price,
                Status = true // Active by default
            };
            // Save the book to the database first
            await _bookRepository.AddBookAsync(book);

            // Now handle tags
            foreach (var tagName in model.Tags)
            {
                // Check if the tag already exists
                var tag = await _bookRepository.GetTagByNameAsync(tagName);
                if (tag == null)
                {
                    // If the tag does not exist, create a new tag
                    tag = new Tag { TagName = tagName };
                    await _bookRepository.AddTagAsync(tag);
                }

                // Create the BookTag relationship
                var bookTag = new BookTag
                {
                    BookID = book.BookID,
                    TagID = tag.TagID
                };

                // Save the BookTag to the database
                await _bookRepository.AddBookTagAsync(bookTag);
            }
            return new BookResponseDto
            {
                BookID = book.BookID,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Image = book.Image,
                Price = book.Price,
                Status = book.Status,
                UserID = book.UserID
            };
        }

        public async Task<BookResponseDto> UpdateBookAsync(int id, BookModel model)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            if (book.UserID != model.UserId)
            {
                throw new Exception("You don't have permission to edit this book");
            }

            // Step 1: Remove existing tags associated with the book
            var existingBookTags = book.BookTags.ToList(); // Assuming BookTags is properly loaded
            foreach (var bookTag in existingBookTags)
            {
                await _bookRepository.RemoveBookTagAsync(bookTag.BookID, bookTag.TagID);
            }

            // Step 2: Update book details
            book.Title = model.Title;
            book.Description = model.Description;
            book.Category = model.Category;
            book.Image = model.Image;
            book.Price = model.Price;

            // Save updated book details to the database
            await _bookRepository.UpdateBookAsync(book);

            // Step 3: Add new tags
            foreach (var tagName in model.Tags)
            {
                // Check if the tag already exists
                var tag = await _bookRepository.GetTagByNameAsync(tagName);
                if (tag == null)
                {
                    // If the tag does not exist, create a new tag
                    tag = new Tag { TagName = tagName };
                    await _bookRepository.AddTagAsync(tag);
                }

                // Create the new BookTag relationship
                var bookTag = new BookTag
                {
                    BookID = book.BookID,
                    TagID = tag.TagID
                };

                // Save the BookTag to the database
                await _bookRepository.AddBookTagAsync(bookTag);
            }

            // Step 4: Return updated book data
            return new BookResponseDto
            {
                BookID = book.BookID,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Image = book.Image,
                Price = book.Price,
                Status = book.Status,
                UserID = book.UserID
            };
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return false;
            }

            await _bookRepository.DeleteBookAsync(book);
            return true;
        }

        // Tag CRUD operations
        public async Task<Tag> GetTagByIdAsync(int tagId)
        {
            return await _bookRepository.GetTagByIdAsync(tagId);
        }

        public async Task<Tag> CreateTagAsync(TagModel model)
        {
            var tag = new Tag
            {
                TagName = model.TagName
            };

            await _bookRepository.AddTagAsync(tag);
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await _bookRepository.GetTagsAsync();
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            await _bookRepository.UpdateTagAsync(tag);
        }

        public async Task DeleteTagAsync(int tagId)
        {
            await _bookRepository.DeleteTagAsync(tagId);
        }

        // BookTag management
        public async Task AddBookTagAsync(int bookId, int tagId)
        {
            var bookTag = new BookTag { BookID = bookId, TagID = tagId };
            await _bookRepository.AddBookTagAsync(bookTag);
        }

        public async Task RemoveBookTagAsync(int bookId, int tagId)
        {
            await _bookRepository.RemoveBookTagAsync(bookId, tagId);
        }
    }

}
