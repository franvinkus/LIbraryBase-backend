using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostBooksHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PostBooksModel> Handle(PostBooksQuery request, CancellationToken cancellationToken)
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdString);
            var newBooks = new Book
            {
                Title = request.title,
                Author = request.author,
                Description = request.description,
                CreatedBy = userId
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
