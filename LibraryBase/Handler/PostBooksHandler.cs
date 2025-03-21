using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class PostBooksHandler : IRequestHandler<PostBooksQuery, PostBooksModel>
    {
        public readonly LibraryBaseContext _db;
        public PostBooksHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<PostBooksModel> Handle(PostBooksQuery request, CancellationToken cancellationToken)
        {
            var newBooks = new Book
            {
                Title = request.title,
                Author = request.author,
                Description = request.description
            };

            var categories = await _db.Categories
                .Where(c => request.categoryIds.Contains(c.CategoryId))
                .ToListAsync(cancellationToken);

            newBooks.Categories = categories;

            _db.Books.Add(newBooks);

            var response = new PostBooksModel
            {
                categoryIds = request.categoryIds,
                title = request.title,
                author = request.author,
                description = request.description
            };

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
