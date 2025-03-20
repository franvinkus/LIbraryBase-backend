using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class GetCategoryQuery : IRequest<List<GetCategoryModel>>
    {
        public int cateId { get; set; }
        public string cateName { get; set; } = string.Empty;
        public string createdAt { get; set; } = string.Empty;
        public string updatedAt { get; set; } = string.Empty;
    }
}
