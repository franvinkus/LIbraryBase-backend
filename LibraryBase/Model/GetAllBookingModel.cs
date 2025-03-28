namespace LibraryBase.Model
{
    public class GetAllBookingModel
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
    }
}
