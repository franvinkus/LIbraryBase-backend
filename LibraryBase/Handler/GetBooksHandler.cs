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
        public GetBooksHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<List<GetBooksModel>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var datas = await _db.Books
                .Include(c => c.Category)
                .Select(x => new GetBooksModel
                {
                    bookId = x.BookId,
                    cateId = x.CategoryId,
                    categoryName = x.Category.CategoryName,
                    title = x.Title,
                    author = x.Author,
                    description = x.Description,
                    createdAt = x.CreatedAt.HasValue ? x.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    updatedAt = x.UpdatedAt.HasValue ? x.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                }).ToListAsync(cancellationToken);

            return datas;
        }
    }
}
