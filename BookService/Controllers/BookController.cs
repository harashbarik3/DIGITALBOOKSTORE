using BookService.Modals.DTO;
using BookService.Repositories;
using CommonLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Route("api/v1/digitalbooks/book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("")]
        [Authorize(Roles = "AUTHER")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books= await _bookRepository.GettAllBooksAsync();

            return Ok(books);
        }

        [HttpGet("{bookid:guid}")]
        public async Task<IActionResult> GetBookById(Guid bookid)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(bookid);

                if (book != null)
                {
                    return Ok(book);
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }          

        }

        [HttpPost("auther/{auther_id:guid}/books")]
        public async Task<IActionResult> CreateBook([FromBody] AddBookRequest bookReq, [FromRoute]Guid auther_id)
        {
            try
            {
                var book = new Book
                {
                    BookName = bookReq.BookName,
                    BookContent = bookReq.BookContent,
                    Price = bookReq.Price,
                    PublishedDate = bookReq.PublishedDate,
                    Active = bookReq.Active,
                    Image =null,
                    CategoryName = bookReq.CategoryName,
                    PublisherName = bookReq.PublisherName,
                    UserId = auther_id                    
                };

                var bookToAdd=await _bookRepository.AddBookAsync(book);

                if (bookToAdd!=null)
                {
                    return Ok(bookToAdd);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("auther/books/update/{book_id:guid}")]
        public async Task<IActionResult> EditBook([FromBody]UpdateBookRequest updatebook, [FromRoute]Guid book_id)
        {
            try
            {
                var book = new Book
                {
                    BookName = updatebook.BookName,
                    BookContent = updatebook.BookContent,
                    Price = updatebook.Price,
                    PublishedDate = updatebook.PublishedDate,
                    Active = updatebook.Active,
                    Image = updatebook.Image,
                    CategoryName = updatebook.CategoryName,
                    PublisherName = updatebook.PublisherName,
                };

                await _bookRepository.UpdateBookAsync(book, book_id);
                return Ok(book);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }

        [HttpDelete("delete/book/{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id); ;

                if (book != null)
                {

                    await _bookRepository.DeleteBookAsync(book.BookId);
                    return Ok(book);
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


    }
}
