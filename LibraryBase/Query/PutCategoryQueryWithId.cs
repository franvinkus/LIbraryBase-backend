using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutCategoryQueryWithId : IRequest<PutCategoryModel>
    {
        public int cateId { get; set; }
        public string cateName { get; set; } = string.Empty;
        public string updatedAt { get; set; } = string.Empty;
        public int updatedBy { get; set; }
    }
}
