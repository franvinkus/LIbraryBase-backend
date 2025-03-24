using System.Text.Json.Serialization;
using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class PutCategoryQuery : IRequest<PutCategoryModel>
    {
        public int cateId { get; set; }
        public string categoryName { get; set; } = string.Empty;
    }
}
