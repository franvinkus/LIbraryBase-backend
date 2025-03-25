using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutReturnBookingQuery : IRequest<PutReturnBookingModel>
    {
        public int id { get; set; }

        public PutReturnBookingQuery(int id)
        {
            this.id = id;
        }
    }
}
