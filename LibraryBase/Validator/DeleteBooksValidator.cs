using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class DeleteBooksValidator : AbstractValidator<DeleteBooksQuery>
    {
        public DeleteBooksValidator() 
        {
            RuleFor(x => x.deletedId)
                .GreaterThan(0)
                .WithMessage("Id starts from 1");
        }
    }
}
