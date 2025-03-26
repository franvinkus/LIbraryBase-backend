using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        public PutBooksHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }
        public async Task<PutBooksModel> Handle(PutBooksQueryWithId request, CancellationToken cancellationToken)
        {
            var book = await _db.Books
                .Where(x => x.BookId == request.bookId)
                .Include(b => b.Categories)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new Exception("There is such book");
            }

            book.Categories.Clear();
            await _db.SaveChangesAsync(cancellationToken);

            var newCategories = await _db.Categories
                .Where(x => request.categoryIds.Contains(x.CategoryId))
                .ToListAsync(cancellationToken);

            foreach (var category in newCategories) 
            {
                book.Categories.Add(category);
            }

            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdString);

            string? imgPath = null;

            if (request.bookImage != null && request.bookImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = $"{Guid.NewGuid()}_{request.bookImage.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.bookImage.CopyToAsync(stream);
                }

                // Simpan path gambar ke database
                book.Img = $"/uploads/{fileName}";
            }


            book.Title = request.title;
            book.Author = request.author;
            book.Description = request.description;
            book.UpdatedAt = DateTime.UtcNow;
            book.UpdatedBy = userId;

            var response = new PutBooksModel
            {
                categoryIds = request.categoryIds,
                title = request.title,
                author = request.author,
                description = request.description,
                updatedBy = userId,
                updatedAt = book.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""
            };

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
