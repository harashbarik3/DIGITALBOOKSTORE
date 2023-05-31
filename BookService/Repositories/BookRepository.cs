using CommonLib.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using UserService;

namespace BookService.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DigitalbookstoreContext _context;
        public BookRepository(DigitalbookstoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GettAllBooksAsync()
        {
            var books = await _context.Books.ToListAsync();

            return books;
        }

        public async Task<Book> GetBookByIdAsync(Guid bookid)
        {
            var book=await _context.Books.Where(x=>x.BookId==bookid).FirstOrDefaultAsync();

            if (book != null)
            {
                return book;
            }
            else
            {
                return null;
            }
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            try
            {
                var category = new Category
                {
                    CategoryId=Guid.NewGuid(),
                    CategoryName=book.CategoryName
                };
                await _context.AddAsync(category);
                await _context.SaveChangesAsync();
                var publisher = new Publisher
                {
                    PublisherId = Guid.NewGuid(),
                    PublisherName = book.PublisherName
                };
                await _context.AddAsync(publisher);
                await _context.SaveChangesAsync();

                book.BookId=Guid.NewGuid();
                book.CategoryId = category.CategoryId;
                book.PublisherId= publisher.PublisherId;
                await _context.AddAsync(book);
                await _context.SaveChangesAsync();

                return book;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Book> UpdateBookAsync(Book book, Guid bookid)
        {
            try
            {
                var existingBook = await _context.Books.Where(x=>x.BookId==bookid).FirstOrDefaultAsync();

                if (existingBook != null)
                {
                    existingBook.BookName = book.BookName;
                    existingBook.BookContent = book.BookContent;
                    existingBook.Price = book.Price;
                    existingBook.CategoryName = book.CategoryName;
                    existingBook.PublisherName = book.PublisherName;
                    existingBook.Active = book.Active;
                    existingBook.PublishedDate= book.PublishedDate;
                    existingBook.Image= book.Image;

                    await _context.SaveChangesAsync();
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<Book> DeleteBookAsync(Guid id)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.BookId == id);

                if (book != null)
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
