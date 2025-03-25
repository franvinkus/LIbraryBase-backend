using Library.Entities;
using LibraryBase.Query;

namespace LibraryBase.Model
{
    public class PostBookingModel
    {
        public int bookingId {  get; set; }
        public int booksId { get; set; }
        public string bookingDate { get; set; } = string.Empty;
        public string pickUpDeadLine { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string msg { get; set; } = string.Empty;
    }
}
