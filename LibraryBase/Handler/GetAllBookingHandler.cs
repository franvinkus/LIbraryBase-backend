using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class GetAllBookingHandler : IRequestHandler<GetAllBookingQuery, List<GetAllBookingModel>>
    {
        public readonly LibraryBaseContext _db;
        public GetAllBookingHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<List<GetAllBookingModel>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            var query = _db.Bookings
                .Include(u => u.User)
                .Include(b => b.Book)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.username))
            {
                query = query.Where(Q => Q.User.Username.Contains(request.username.Trim()));
            }

            var count = await query.CountAsync();

            var datas = await query
                .Skip((request.pageNumber - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(q => new GetAllBookingModel
                {
                    bookingId = q.BookingId,
                    userId = q.UserId,
                    username = q.User.Username,
                    bookId = q.BookId,
                    title = q.Book.Title,
                    returnDate = q.ReturnDate.HasValue ? q.ReturnDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    status = q.Status
                }).ToListAsync(cancellationToken);

            return datas;
        }
    }
}
