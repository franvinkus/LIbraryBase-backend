using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class SignupAdminHandler : IRequestHandler<SignupAdminQuery, SignupAdminModel>
    {
        public readonly LibraryBaseContext _db;
        public SignupAdminHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<SignupAdminModel> Handle(SignupAdminQuery request, CancellationToken cancellationToken)
        {
            var defaultRole = await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin", cancellationToken);
            var newUser = new User
            {
                Username = request.userName,
                Email = request.email,
                PasswordHash = request.password,
                RoleId = defaultRole.RoleId
            };

            _db.Users.Add(newUser);

            var response = new SignupAdminModel
            {
                userName = request.userName,
                Email = request.email,
                CreatedAt = DateTime.Now
            };


            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
