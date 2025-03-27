using System.Security.Claims;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class PostBookingHandler : IRequestHandler<PostBookingQuery, PostBookingModel>
    {
        private readonly LibraryBaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostBookingHandler(LibraryBaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PostBookingModel> Handle(PostBookingQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("DEBUG: PostBookingHandler.Handle() is called");
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User not found");
            }
            var userId = int.Parse(userIdString);

            var userExists = await _db.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
            if (!userExists)
            {
                throw new KeyNotFoundException("User not found in the database.");
            }

            var book = await _db.Books
                .Where(x => x.BookId == request.id)
                .FirstOrDefaultAsync(cancellationToken);

            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            if(book.Availability == false)
            {
                throw new InvalidOperationException("This book is already borrowed.");
            }

            var existingBook = await _db.Bookings
                .Where(x => x.BookId == request.id && x.UserId == userId && x.ReturnDate == null)
                .FirstOrDefaultAsync(cancellationToken);

            Console.WriteLine($"Debug: Checking existing booking for UserId {userId} and BookId {request.id}");
            Console.WriteLine(existingBook != null
                ? $"Found booking with BookingId {existingBook.BookId}, Status: {existingBook.Status}"
                : "No existing booking found.");

            if (existingBook != null)
            {
                throw new InvalidOperationException("You have already borrowed this book and not returned it yet.");
            }

            var now = DateTime.UtcNow;
            var pickupDeadline = now.AddDays(2);

            var newBooking = new Booking
            {
                BookId = request.id,
                UserId = userId,
                BookingDate = now,
                PickupDeadline = pickupDeadline,
                Status = "pending",
                CreatedAt = now,
                CreatedBy = userId
            };

            _db.Bookings.Add(newBooking);
            await _db.SaveChangesAsync(cancellationToken);

            book.Availability = false;
            book.AvailabilityDate = now.AddDays(9);
            await _db.SaveChangesAsync(cancellationToken);

            return new PostBookingModel
            {
                bookingId = newBooking.BookingId,
                booksId = newBooking.BookId,
                bookingDate = newBooking.BookingDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                pickUpDeadLine = newBooking.PickupDeadline?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                status = newBooking.Status,
                msg = "Book successfully borrowed! Please pick it up before the deadline."
            };
        }
    }
}
