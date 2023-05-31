using CommonLib.Models;

namespace BookService.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GettAllBooksAsync();
        Task<Book> GetBookByIdAsync(Guid bookid);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book, Guid bookid);
        Task<Book> DeleteBookAsync(Guid id);
    }
}
