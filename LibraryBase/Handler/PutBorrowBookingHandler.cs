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
            .Include(u => u.User)
            .Where(b => b.BookingId == request.id && b.UserId == user.UserId)
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
            await _db.SaveChangesAsync(cancellationToken);

            return new PutBorrowBookingModel
            {
                bookingId = booking.BookingId,
                booksId = booking.BookId,
                status = booking.Status,
                msg = $"{user.Username} has picked up the book."
            };
        }
    }
}
