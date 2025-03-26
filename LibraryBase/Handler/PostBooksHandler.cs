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
        private readonly IWebHostEnvironment _env;
        public PostBooksHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }
        public async Task<PostBooksModel> Handle(PostBooksQuery request, CancellationToken cancellationToken)
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdString);

            string? imgPath = null;

            if (request.imgFile != null)
            {
                var fileName = $"{Guid.NewGuid()}_{request.imgFile.FileName}";
                var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.imgFile.CopyToAsync(stream);
                }

                imgPath = $"/images/{fileName}"; 
            }

            var categories = await _db.Categories
                .Where(c => request.categoryIds.Contains(c.CategoryId))
                .ToListAsync(cancellationToken);

            var newBooks = new Book
            {
                Title = request.title,
                Author = request.author,
                Description = request.description,
                CreatedBy = userId,
                Img = imgPath
            };

            newBooks.Categories = categories;

            _db.Books.Add(newBooks);

            var response = new PostBooksModel
            {
                categoryIds = request.categoryIds,
                title = request.title,
                author = request.author,
                description = request.description,
                img = newBooks.Img
            };

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
