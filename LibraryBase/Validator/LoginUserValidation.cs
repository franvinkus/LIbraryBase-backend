using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class LoginUserValidation : AbstractValidator<LoginUserQuery>
    {
        public LoginUserValidation() 
        {
            RuleFor(x => x.password)
                .NotEmpty()
                .WithMessage("This field must be filled");

            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.userName) || !string.IsNullOrEmpty(x.email))
                .WithMessage("Either Username or Email is required");
        }
    }
}
