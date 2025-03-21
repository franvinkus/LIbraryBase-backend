using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class DeleteBooksQuery : IRequest<DeleteBooksModel>
    {
        public int deletedId { get; set; }
        public DeleteBooksQuery(int deletedId)
        {
            this.deletedId = deletedId;
        }
    }
}
