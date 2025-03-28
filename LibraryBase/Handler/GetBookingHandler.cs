using System.Security.Claims;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class GetBookingHandler : IRequestHandler<GetBookingQuery, List<GetBookingModel>>
    {
        public readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetBookingHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<GetBookingModel>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

            var userIdString = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userId = int.Parse(userIdString);

            var bookings = await _db.Bookings
                .Include(b => b.Book)
                .ThenInclude(c => c.Categories)
                .Where(b => b.UserId == userId)
                .ToListAsync(cancellationToken);
            
            var bookinglist = bookings
                .Select(b => new GetBookingModel
                {
                    bookingId = b.BookingId,
                    bookId = b.BookId,
                    bookTitle = b.Book.Title,
                    bookImage = string.IsNullOrEmpty(b.Book.Img)
                    ? null
                        : $"{baseUrl}{b.Book.Img}",
                    categories = b.Book.Categories.Select(c => c.CategoryName).ToList(),
                    status = b.Status,
                    createdAt = b.CreatedAt.HasValue ? b.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    updatedAt = b.UpdatedAt.HasValue ? b.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""
                })
                .ToList();

            if (bookinglist == null)
            {
                throw new KeyNotFoundException($"Booking dengan ID {request.bookingId} tidak ditemukan.");
            }

            return bookinglist;

        }
    }
}
