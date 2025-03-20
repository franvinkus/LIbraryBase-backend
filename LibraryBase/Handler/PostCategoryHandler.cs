using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;

namespace LibraryBase.Handler
{
    public class PostCategoryHandler : IRequestHandler<PostCategoryQuery, PostCategoryModel>
    {
        public readonly LibraryBaseContext _db;
        public PostCategoryHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<PostCategoryModel> Handle(PostCategoryQuery request, CancellationToken cancellationToken)
        {
            var newCategory = new Category
            {
                CategoryName = request.categoryName,
                CreatedBy = request.createdBy,
            };

            _db.Categories.Add(newCategory);

            var response = new PostCategoryModel
            {
                categoryName = request.categoryName,
                createdBy = request.createdBy
            };

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
