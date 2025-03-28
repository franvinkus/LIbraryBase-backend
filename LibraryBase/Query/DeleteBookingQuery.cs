using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class DeleteBookingQuery : IRequest<DeleteBookingModel>
    {
        public int userId { get; set; }
        public int deletedId { get; set; }

        public DeleteBookingQuery(int userId, int deletedId)
        {
            this.userId = userId;
            this.deletedId = deletedId;
        }
    }
}
