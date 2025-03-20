using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class DeleteCategoryQuery : IRequest<DeleteCategoryModel>
    {
        public int deletedCateId { get; set; }
        public DeleteCategoryQuery(int cateId)
        {
            this.deletedCateId = cateId;
        }
    }
}
