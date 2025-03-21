using System.ComponentModel.DataAnnotations;

namespace LibraryBase.Model
{
    public class DeleteBooksModel
    {
        [Required]
        public int deletedId {get; set;}
    }
}
