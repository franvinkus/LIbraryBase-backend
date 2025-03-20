using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutCategoryQuery : IRequest<PutCategoryModel>
    {
        public string categoryName { get; set; } = string.Empty;
        public int updatedBy { get; set; }
    }
}
