using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LibraryBase.Model
{
    public class PutBooksModel
    {
        [Required]
        public List<int> categoryIds { get; set; } = new();
        [Required]
        public string title { get; set; } = string.Empty;
        [Required]
        public string author { get; set; } = string.Empty;
        [Required]
        public string description { get; set; } = string.Empty;
        [Required]
        public int? updatedBy { get; set; }
        [Required]
        public string updatedAt { get; set; } = string.Empty;
    }
}
