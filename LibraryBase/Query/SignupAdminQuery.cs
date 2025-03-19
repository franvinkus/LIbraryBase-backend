using LibraryBase.Model;
using MediatR;

namespace LibraryBase.Query
{
    public class SignupAdminQuery : IRequest<SignupAdminModel>
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}
