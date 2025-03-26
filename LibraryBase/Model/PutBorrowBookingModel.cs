namespace LibraryBase.Model
{
    public class PutBorrowBookingModel
    {
        public int bookingId { get; set; }
        public int booksId { get; set; }
        public string status { get; set; } = string.Empty;
        public string msg { get; set; } = string.Empty;
    }
}
