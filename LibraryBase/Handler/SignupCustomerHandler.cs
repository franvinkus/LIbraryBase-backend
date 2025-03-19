using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class SignupCustomerHandler : IRequestHandler<SignupCustomerQuery, SignupCustomerModel>
    {
        public readonly LibraryBaseContext _db;
        public SignupCustomerHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<SignupCustomerModel> Handle(SignupCustomerQuery request, CancellationToken cancellationToken)
        {
            var defaultRole = await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == "Customer", cancellationToken);
            var newUser = new User
            {
                Username = request.userName,
                Email = request.email,
                PasswordHash = request.password,
                RoleId = defaultRole.RoleId
            };

            _db.Users.Add(newUser);

            var response = new SignupCustomerModel
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
