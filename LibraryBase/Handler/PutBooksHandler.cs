using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class PutBooksHandler : IRequestHandler<PutBooksQueryWithId, PutBooksModel>
    {
        public readonly LibraryBaseContext _db;
        public PutBooksHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<PutBooksModel> Handle(PutBooksQueryWithId request, CancellationToken cancellationToken)
        {
            var book = await _db.Books
                .Where(x => x.BookId == request.bookId)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new Exception("There is such book");
            }

            book.Categories.Clear();

            var newCategories = await _db.Categories
                .Where(x => request.categoryIds.Contains(x.CategoryId))
                .ToListAsync(cancellationToken);

            foreach (var category in newCategories) 
            {
                book.Categories.Add(category);
            }

            book.Title = request.title;
            book.Author = request.author;
            book.Description = request.description;
            book.UpdatedAt = DateTime.UtcNow;
            book.UpdatedBy = request.updatedBy;

            var response = new PutBooksModel
            {
                categoryIds = request.categoryIds,
                title = request.title,
                author = request.author,
                description = request.description,
                updatedBy = request.updatedBy,
                updatedAt = book.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""
            };

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
