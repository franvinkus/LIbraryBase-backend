using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutCategoryQueryWithId : IRequest<PutCategoryModel>
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public int updatedBy { get; set; }
    }
}
