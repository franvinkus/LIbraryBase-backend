using System.ComponentModel.DataAnnotations;

namespace LibraryBase.Model
{
    public class SignupCustomerModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string userName { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        [RegularExpression(@"^(?=.*\d).{8,20}$", ErrorMessage = "Password harus mengandung minimal 1 angka")]
        public string password { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Valid email must contains '@'")]
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
