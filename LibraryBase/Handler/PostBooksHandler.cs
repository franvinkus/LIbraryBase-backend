using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;

namespace LibraryBase.Handler
{
    public class PostBooksHandler : IRequestHandler<PostBooksQuery, PostBooksModel>
    {
        public readonly LibraryBaseContext _db;
        public PostBooksHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<PostBooksModel> Handle(PostBooksQuery request, CancellationToken cancellationToken)
        {
            var newBooks = new Book
            {
                CategoryId = request.cateId,
                Title = request.title,
                Author = request.author,
                Description = request.description
            };

            _db.Books.Add(newBooks);

            var response = new PostBooksModel
            {
                cateId = request.cateId,
                title = request.title,
                author = request.author,
                description = request.description
            };

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
