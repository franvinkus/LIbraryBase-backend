using System.Security.Claims;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;

namespace LibraryBase.Handler
{
    public class PostCategoryHandler : IRequestHandler<PostCategoryCommand, PostCategoryModel>
    {
        public readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostCategoryHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PostCategoryModel> Handle(PostCategoryCommand request, CancellationToken cancellationToken)
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdString);

            var newCategory = new Category
            {
                CategoryName = request.categoryName,
                CreatedBy = userId
            };

            _db.Categories.Add(newCategory);

            var response = new PostCategoryModel
            {
                categoryName = request.categoryName
            };

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
