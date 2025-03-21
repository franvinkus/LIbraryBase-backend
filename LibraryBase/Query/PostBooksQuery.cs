using LibraryBase.Model;
using MediatR;
using Microsoft.Extensions.Primitives;

namespace LibraryBase.Query
{
    public class PostBooksQuery : IRequest<PostBooksModel>
    {
        public int cateId { get; set; }
        public string title { get; set; } = string.Empty;
        public string author { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
    }
}
