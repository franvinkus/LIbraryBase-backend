using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class PutCategoryHandler : IRequestHandler<PutCategoryQuery, PutCategoryModel>
    {
        public readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PutCategoryHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PutCategoryModel> Handle(PutCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _db.Categories
                .Where(x => x.CategoryId == request.cateId)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new Exception("Id is not found");
            }

            var httpContext = _httpContextAccessor.HttpContext;
            var updatedBy = httpContext?.Session.GetInt32("UserId") ?? 0;

            category.CategoryName = request.categoryName;
            category.UpdatedAt = DateTime.UtcNow;
            category.UpdatedBy = updatedBy;

            var updatedData = new PutCategoryModel
            {
                categoryName = category.CategoryName,
                updatedAt = category.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                updatedBy = updatedBy,
            };

            await _db.SaveChangesAsync(cancellationToken);
            return updatedData;
        }
    }
}
