using System.ComponentModel.DataAnnotations;
using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutBooksQuery : IRequest<PutBooksModel>
    {

        public int cateId { get; set; }

        public string title { get; set; } = string.Empty;

        public string author { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public int? updatedBy { get; set; }
    }
}
