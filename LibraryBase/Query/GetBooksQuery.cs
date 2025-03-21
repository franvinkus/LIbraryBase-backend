using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class GetBooksQuery : IRequest<List<GetBooksModel>>
    {
        public int bookId { get; set; }
        public int cateId { get; set; }
        public string title { get; set; } = string.Empty;
        public string author { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string categoryName { get; set; } = string.Empty;
        public string createdAt { get; set; } = string.Empty;
        public string updatedAt { get; set; } = string.Empty;
    }
}
