using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutBorrowBookingQuery : IRequest<PutBorrowBookingModel>
    {
        public int id { get; set; }

        public PutBorrowBookingQuery(int id)
        {
            this.id = id;
        }
    }
}
