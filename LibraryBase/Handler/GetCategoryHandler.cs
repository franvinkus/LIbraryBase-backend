using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, List<GetCategoryModel>>
    {
        public readonly LibraryBaseContext _db;
        public GetCategoryHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<List<GetCategoryModel>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var data = await _db.Categories
                .Select(x => new 
                {
                    x.CategoryId,
                    x.CategoryName,
                    x.CreatedAt,
                    x.UpdatedAt 
                }).ToListAsync(cancellationToken);

            var result = data.Select(x => new GetCategoryModel
            {
                cateId = x.CategoryId,
                cateName = x.CategoryName,
                createdAt = x.CreatedAt.HasValue ? x.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                updatedAt = x.UpdatedAt.HasValue ? x.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""
            }).ToList();

            return result;
        }
    }
}
