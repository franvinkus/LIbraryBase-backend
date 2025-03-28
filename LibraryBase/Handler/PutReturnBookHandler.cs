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
            var userRole = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can update booking status.");
            }

            var user = await _db.Users
                .Where(x => x.UserId == request.userId)
                .FirstOrDefaultAsync(cancellationToken);


            var booking = await _db.Bookings
            .Include(b => b.Book)
            .Where(b => b.BookingId == request.id && b.UserId == user.UserId)
            .FirstOrDefaultAsync(cancellationToken);

            if (booking == null)
            {
                throw new Exception("Booking not found");
            }

            if (booking.Status == "returned")
            {
                throw new Exception("Book already returned");
            }

            booking.Status = "returned";
            booking.ReturnDate = DateTime.UtcNow;

            var book = booking.Book;
            book.Availability = true;
            book.AvailabilityDate = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);

            return new PutReturnBookingModel
            {
                bookingId = booking.BookingId,
                booksId = booking.BookId,
                returnDate = booking.ReturnDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                status = booking.Status,
                msg = $"{user.Username} has returned up the book."
            };
        }
    }
}
