namespace LibraryBase.Model
{
    public class PutReturnBookingModel
    {
        public int bookingId { get; set; }
        public int booksId { get; set; }
        public string returnDate { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string msg { get; set; } = string.Empty;
    }
}
