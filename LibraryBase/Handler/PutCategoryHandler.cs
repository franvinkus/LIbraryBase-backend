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
        public PutCategoryHandler(LibraryBaseContext db)
        {
            _db = db;            
        }
        public async Task<PutCategoryModel> Handle(PutCategoryQueryWithId request, CancellationToken cancellationToken)
        {
            var category = await _db.Categories
                .Where(x => x.CategoryId == request.categoryId)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new Exception("Id is not found");
            }

            category.CategoryName = request.categoryName;
            category.UpdatedAt = DateTime.UtcNow;
            category.UpdatedBy = request.updatedBy;

            var updatedData = new PutCategoryModel
            {
                categoryName = category.CategoryName,
                updatedAt = category.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                updatedBy = request.updatedBy,
            };

            await _db.SaveChangesAsync(cancellationToken);
            return updatedData;
        }
    }
}
