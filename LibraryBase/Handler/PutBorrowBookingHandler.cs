using System.Security.Claims;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class PutBorrowBookingHandler : IRequestHandler<PutBorrowBookingQuery, PutBorrowBookingModel>
    {
        private readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutBorrowBookingHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PutBorrowBookingModel> Handle(PutBorrowBookingQuery request, CancellationToken cancellationToken)
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

            if (booking.Status == "borrowed")
            {
                throw new Exception("Book already picked up");
            }

            if (booking.Status == "returned")
            {
                throw new Exception("Book already returned");
            }

            booking.Status = "borrowed";
            booking.ReturnDate = DateTime.UtcNow;

            var book = booking.Book;
            book.Availability = true;
            await _db.SaveChangesAsync(cancellationToken);

            return new PutBorrowBookingModel
            {
                bookingId = booking.BookingId,
                booksId = booking.BookId,
                returnDate = booking.ReturnDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                status = booking.Status,
                msg = "You have picked up the book. Don't Forget to return it before the deadline!"
            };
        }
    }
}
