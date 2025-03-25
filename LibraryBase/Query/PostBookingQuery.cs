using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PostBookingQuery : IRequest<PostBookingModel>
    {
        public int id { get; set; }

        public PostBookingQuery(int id)
        {
            this.id = id;
        }
    }
}
