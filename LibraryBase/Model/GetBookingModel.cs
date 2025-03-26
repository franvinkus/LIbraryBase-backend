namespace LibraryBase.Model
{
    public class GetBookingModel
    {
        public int bookingId { get; set; }
        public int bookId { get; set; }
        public string bookTitle { get; set; } = string.Empty;
        public string bookImage { get; set; } = string.Empty;
        public List<string> categories { get; set; } = new List<string>();
        public string bookingDate { get; set; } = string.Empty;
        public string pickUpDeadline { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string createdAt { get; set; } = string.Empty;
        public string updatedAt { get; set; } = string.Empty;
    }
}
