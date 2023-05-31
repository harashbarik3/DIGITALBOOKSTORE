using CommonLib.Models;

namespace BookService.Modals.DTO
{
    public class AddBookRequest
    {
        public string BookName { get; set; } = null!;        

        public decimal Price { get; set; }

        public string PublishedDate { get; set; } = null!;

        public string BookContent { get; set; } = null!;

        public Guid UserId { get; set; }

        public byte[]? Image { get; set; } = null;

        public string? CategoryName { get; set; }

        public string? PublisherName { get; set; }
        public bool Active { get; set; }
    }
}
