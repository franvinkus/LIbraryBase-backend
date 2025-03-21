namespace LibraryBase.Model
{
    public class GetBooksModel
    {
        public int bookId { get; set; }
        public int cateId { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string author { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string createdAt { get; set; } = string.Empty;
        public string updatedAt { get; set; } = string.Empty;
    }
}
