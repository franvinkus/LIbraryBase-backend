using System.ComponentModel.DataAnnotations;

namespace LibraryBase.Model
{
    public class PutCategoryModel
    {
        [Required]
        public string categoryName {  get; set; } = string.Empty;
        [Required]
        public string updatedAt { get; set; } = string.Empty;
        [Required]
        public int? updatedBy { get; set; }
    }
}
