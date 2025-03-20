using System.ComponentModel.DataAnnotations;

namespace LibraryBase.Model
{
    public class PostCategoryModel
    {
        [Required]
        public string categoryName { get; set; } = string.Empty;
        [Required]
        public int createdBy { get; set; }
    }
}
