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

        // Book CRUD endpoints
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

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CreateBook([FromBody] BookModel model)
        {
            var book = await _bookService.CreateBookAsync(model);
            return Ok(book);
        }

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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        // Tag CRUD endpoints
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

        [HttpPost("tag")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTag([FromBody] TagModel model)
        {
            var tag = await _bookService.CreateTagAsync(model);
            return Ok(tag);
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _bookService.GetTagsAsync();
            return Ok(tags);
        }

        [HttpPut("tags/{tagId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTag(int tagId, [FromBody] Tag tag)
        {
            if (tagId != tag.TagID)
            {
                return BadRequest();
            }
            await _bookService.UpdateTagAsync(tag);
            return Ok();
        }

        [HttpDelete("tags/{tagId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            await _bookService.DeleteTagAsync(tagId);
            return Ok();
        }

        // BookTag management
        [HttpPost("{bookId}/tags/{tagId}")]
        public async Task<IActionResult> AddBookTag(int bookId, int tagId)
        {
            await _bookService.AddBookTagAsync(bookId, tagId);
            return NoContent();
        }

        [HttpDelete("{bookId}/tags/{tagId}")]
        public async Task<IActionResult> RemoveBookTag(int bookId, int tagId)
        {
            await _bookService.RemoveBookTagAsync(bookId, tagId);
            return NoContent();
        }
    }

}
