using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class DeleteBookingValidator : AbstractValidator<DeleteBookingQuery>
    {
        public DeleteBookingValidator()
        {
            RuleFor(x => x.deletedId)
                .GreaterThan(0)
                .WithMessage("Please input the correct BookingID");
        }
    }
}
