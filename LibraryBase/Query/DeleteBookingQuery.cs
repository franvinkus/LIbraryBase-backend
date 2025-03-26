using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class DeleteBookingQuery : IRequest<DeleteBookingModel>
    {
        public int deletedId { get; set; }

        public DeleteBookingQuery(int deletedId)
        {
            this.deletedId = deletedId;
        }
    }
}
