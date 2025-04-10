﻿using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryBase.Handler
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, LoginUserModel>
    {
        public readonly LibraryBaseContext _db;
        public LoginUserHandler(LibraryBaseContext db)
        {
            _db = db;
        }
        public async Task<LoginUserModel> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(x =>
                    x.Username == request.userName || 
                    x.Email == request.email, cancellationToken);

            if (user == null)
            {
                throw new Exception("User not Found");
            }

            if(user.PasswordHash != request.password)
            {
                throw new Exception("Wrong password");
            }

            return new LoginUserModel
            {
                userName = request.userName,
                email = request.email,
                msg = $"Login Successful, {user.Username}, {user.Role.RoleName}"
            };
        }
    }
}
