using System.Security.Claims;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class PutReturnBookHandler : IRequestHandler<PutReturnBookingQuery, PutReturnBookingModel>
    {
        private readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutReturnBookHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PutReturnBookingModel> Handle(PutReturnBookingQuery request, CancellationToken cancellationToken)
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var userId = int.Parse(userIdString);


            var booking = await _db.Bookings
            .Include(b => b.Book)
            .Where(b => b.BookingId == request.id && b.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);

            if (booking == null)
            {
                throw new Exception("Booking not found");
            }

            if (booking.Status == "Returned")
            {
                throw new Exception("Book already returned");
            }

            booking.Status = "returned";
            booking.ReturnDate = DateTime.UtcNow;

            var book = booking.Book;
            book.Availability = true;
            await _db.SaveChangesAsync(cancellationToken);

            return new PutReturnBookingModel
            {
                bookingId = booking.BookingId,
                booksId = booking.BookId,
                returnDate = booking.ReturnDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                status = booking.Status,
                msg = "Book successfully borrowed! Please pick it up before the deadline."
            };
        }
    }
}
