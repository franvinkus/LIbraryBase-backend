using System.Runtime.InteropServices;
using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class GetAllBookingQuery : IRequest<List<GetAllBookingModel>>
    {
        public int bookingId { get; set; }
        public int userId { get; set; }
        public string username { get; set; } = string.Empty;
        public int bookId { get; set; }
        public string title { get; set; } = string.Empty;
        public string returnDate { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 1;

        public GetAllBookingQuery(string username, int pageNumber, int pageSize)
        {
            this.username = username;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
    }
}
