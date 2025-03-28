using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutBorrowBookingQuery : IRequest<PutBorrowBookingModel>
    {
        public int userId { get; set; }
        public int id { get; set; }

        public PutBorrowBookingQuery(int userId, int id)
        {
            this.userId = userId;
            this.id = id;
        }
    }
}
