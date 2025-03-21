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
            var isBookExist = await _db.Books
                .Where(x => x.BookId == request.bookId)
                .FirstOrDefaultAsync();

            if (isBookExist == null)
            {
                throw new Exception("There is such book");
            }

            isBookExist.CategoryId = request.cateId;
            isBookExist.Title = request.title;
            isBookExist.Author = request.author;
            isBookExist.Description = request.description;
            isBookExist.UpdatedAt = DateTime.UtcNow;
            isBookExist.UpdatedBy = request.updatedBy;

            var response = new PutBooksModel
            {
                cateId = request.cateId,
                title = request.title,
                author = request.author,
                description = request.description,
                updatedBy = request.updatedBy,
                updatedAt = isBookExist.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""
            };

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
