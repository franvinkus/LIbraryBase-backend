using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LibraryBase.Model
{
    public class PostBooksModel
    {
        [Required]
        public int cateId { get; set; }
        [Required]
        public string title { get; set; } = string.Empty;
        [Required]
        public string author { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string description { get; set; } = string.Empty;
        
    }
}
