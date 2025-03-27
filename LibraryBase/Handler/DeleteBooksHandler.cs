using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class DeleteBooksHandler : IRequestHandler<DeleteBooksQuery, DeleteBooksModel>
    {
        public readonly LibraryBaseContext _db;
        public DeleteBooksHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<DeleteBooksModel> Handle(DeleteBooksQuery request, CancellationToken cancellationToken)
        {
            var book = await _db.Books
                .Where(x => x.BookId == request.deletedId)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new Exception("There's no such book");
            }

            bool hasActiveBookings = await _db.Bookings.AnyAsync(b => b.BookId == request.deletedId);
            if (hasActiveBookings)
            {
                throw new Exception("Cannot delete this book because it is still referenced in active bookings.");
            }

            _db.Books.Remove(book);
            await _db.SaveChangesAsync(cancellationToken);

            return new DeleteBooksModel
            {
                deletedId = request.deletedId
            };
        }
    }
}
