using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutReturnBookingQuery : IRequest<PutReturnBookingModel>
    {
        public int userId { get; set; }
        public int id { get; set; }

        public PutReturnBookingQuery(int userId, int id)
        {
            this.userId = userId;
            this.id = id;
        }
    }
}
