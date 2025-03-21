using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutBooksQueryWithId : IRequest<PutBooksModel>
    {
        public int bookId { get; set; }
        public int cateId { get; set; }

        public string title { get; set; } = string.Empty;

        public string author { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public int? updatedBy { get; set; }
    }
}
