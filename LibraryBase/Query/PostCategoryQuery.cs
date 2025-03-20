using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PostCategoryQuery : IRequest<PostCategoryModel>
    {
        public string categoryName { get; set; } = string.Empty;
        public int createdBy { get; set; }
    }
}
