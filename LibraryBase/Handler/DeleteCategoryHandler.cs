using System.Security.Cryptography;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryQuery, DeleteCategoryModel>
    {
        public readonly LibraryBaseContext _db;
        public DeleteCategoryHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<DeleteCategoryModel> Handle(DeleteCategoryQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _db.Categories
                .Where(x => x.CategoryId == request.deletedCateId)
                .FirstOrDefaultAsync();

            if (isExist == null)
            {
                throw new Exception("There is no such Id");
            }

            _db.Categories.Remove(isExist);
            await _db.SaveChangesAsync(cancellationToken); 

            return new DeleteCategoryModel
            {
                deletedCateId = request.deletedCateId
            };
        }
    }
}
