using System.Buffers.Text;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class GetBooksHandler : IRequestHandler<GetBooksQuery, List<GetBooksModel>>
    {
        public readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetBooksHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<GetBooksModel>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

            var datas = await _db.Books
                .Include(c => c.Categories)
                .Select(x => new GetBooksModel
                {
                    bookId = x.BookId,
                    categoryIds = x.Categories.Select(c => c.CategoryId).ToList(),
                    categoryNames = x.Categories.Select(c => c.CategoryName).ToList(),
                    title = x.Title,
                    author = x.Author,
                    description = x.Description,
                    createdAt = x.CreatedAt.HasValue ? x.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    updatedAt = x.UpdatedAt.HasValue ? x.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    imageUrl = string.IsNullOrEmpty(x.Img)
                    ? null
                        : $"{baseUrl}{x.Img}"
                }).ToListAsync(cancellationToken);

            return datas;
        }
    }
}
