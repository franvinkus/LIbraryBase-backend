using System.ComponentModel.DataAnnotations;

namespace LibraryBase.Model
{
    public class LoginUserModel
    {
        [Required]
        public string userName {  get; set; } = string.Empty;
        [Required]
        public string email { get; set; } = string.Empty;
        public string msg { get; set; } = string.Empty;
    }
}
