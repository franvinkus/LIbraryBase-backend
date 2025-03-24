using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PostCategoryCommand : IRequest<PostCategoryModel>
    {
        public string categoryName { get; set; } = string.Empty;
        public int createdBy { get; set; }
    }
}
