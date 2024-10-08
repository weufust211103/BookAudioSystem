using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAudioSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Get a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The book with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        /// <summary>
        /// Get all books.
        /// </summary>
        /// <returns>All books.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Create a new book.
        /// </summary>
        /// <param name="model">The model containing the book details.</param>
        /// <returns>The created book.</returns>
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CreateBook([FromBody] BookModel model)
        {
            var book = await _bookService.CreateBookAsync(model);
            return Ok(book);
        }

        /// <summary>
        /// Update a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <param name="model">The model containing the updated book details.</param>
        /// <returns>The updated book.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookModel model)
        {
            var book = await _bookService.UpdateBookAsync(id, model);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        /// <summary>
        /// Delete a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Get a tag by its ID.
        /// </summary>
        /// <param name="tagId">The ID of the tag.</param>
        /// <returns>The tag with the specified ID.</returns>
        [HttpGet("tags/{tagId}")]
        public async Task<IActionResult> GetTagById(int tagId)
        {
            var tag = await _bookService.GetTagByIdAsync(tagId);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        /// <summary>
        /// Create a new tag.
        /// </summary>
        /// <param name="model">The model containing the tag details.</param>
        /// <returns>The created tag.</returns>
        [HttpPost("tag")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTag([FromBody] TagModel model)
        {
            var tag = await _bookService.CreateTagAsync(model);
            return Ok(tag);
        }

        /// <summary>
        /// Get all tags.
        /// </summary>
        /// <returns>All tags.</returns>
        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _bookService.GetTagsAsync();
            return Ok(tags);
        }

        /// <summary>
        /// Update a tag by its ID.
        /// </summary>
        /// <param name="tagId">The ID of the tag.</param>
        /// <param name="tag">The updated tag.</param>
        /// <returns>No content.</returns>
        [HttpPut("tags/{tagId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTag(int tagId, [FromBody] Tag tag)
        {
            if (tagId != tag.TagID)
            {
                return BadRequest();
            }
            await _bookService.UpdateTagAsync(tag);
            return NoContent();
        }

        /// <summary>
        /// Delete a tag by its ID.
        /// </summary>
        /// <param name="tagId">The ID of the tag.</param>
        /// <returns>No content.</returns>
        [HttpDelete("tags/{tagId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            await _bookService.DeleteTagAsync(tagId);
            return NoContent();
        }

        /// <summary>
        /// Add a tag to a book.
        /// </summary>
        /// <param name="bookId">The ID of the book.</param>
        /// <param name="tagId">The ID of the tag.</param>
        /// <returns>No content.</returns>
        [HttpPost("{bookId}/tags/{tagId}")]
        public async Task<IActionResult> AddBookTag(int bookId, int tagId)
        {
            await _bookService.AddBookTagAsync(bookId, tagId);
            return NoContent();
        }

        /// <summary>
        /// Remove a tag from a book.
        /// </summary>
        /// <param name="bookId">The ID of the book.</param>
        /// <param name="tagId">The ID of the tag.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{bookId}/tags/{tagId}")]
        public async Task<IActionResult> RemoveBookTag(int bookId, int tagId)
        {
            await _bookService.RemoveBookTagAsync(bookId, tagId);
            return NoContent();
        }
    }

}
