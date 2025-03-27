using System.Security.Claims;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class DeleteBookingHandler : IRequestHandler<DeleteBookingQuery, DeleteBookingModel>
    {
        public readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteBookingHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<DeleteBookingModel> Handle(DeleteBookingQuery request, CancellationToken cancellationToken)
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var userId = int.Parse(userIdString);

            var status = await _db.Bookings
            .Include(b => b.Book)
            .Where(b => b.BookingId == request.deletedId && b.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);

            if (status == null)
            {
                throw new Exception("Booking not found");
            }

            _db.Bookings.Remove(status);

            var book = status.Book;
            book.Availability = true;
            await _db.SaveChangesAsync(cancellationToken);

            return new DeleteBookingModel
            {
                deletedId = request.deletedId
            };
        }
    }
}
