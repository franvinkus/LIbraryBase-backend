using System.ComponentModel.DataAnnotations;
using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutBooksQuery : IRequest<PutBooksModel>
    {

        public List<int> categoryIds { get; set; } = new();

        public string title { get; set; } = string.Empty;

        public string author { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;
    }
}
