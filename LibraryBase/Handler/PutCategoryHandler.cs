using System.Security.Claims;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class PutCategoryHandler : IRequestHandler<PutCategoryQueryWithId, PutCategoryModel>
    {
        public readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PutCategoryHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PutCategoryModel> Handle(PutCategoryQueryWithId request, CancellationToken cancellationToken)
        {
            var category = await _db.Categories
                .Where(x => x.CategoryId == request.cateId)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new Exception("Id is not found");
            }

            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdString);

            category.CategoryName = request.cateName;
            category.UpdatedAt = DateTime.UtcNow;
            category.UpdatedBy = userId;

            var updatedData = new PutCategoryModel
            {
                categoryName = category.CategoryName,
                updatedAt = category.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                updatedBy = userId,
            };

            await _db.SaveChangesAsync(cancellationToken);
            return updatedData;
        }
    }
}
