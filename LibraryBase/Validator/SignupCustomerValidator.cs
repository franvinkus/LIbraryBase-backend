using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class SignupCustomerValidator : AbstractValidator<SignupCustomerQuery>
    {
        public SignupCustomerValidator()
        {
            RuleFor(x => x.userName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(25)
                .WithMessage("Username length must be between 5 and 25 characters");

            RuleFor(x => x.password)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(25)
                .Matches(@".*\d.*")
                .WithMessage("password length must be between 8 and 20 characters And mixed with numbers");

            RuleFor(x => x.email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Valid email must contain '@'");

        }
    }
}
