using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class PutBooksValidator : AbstractValidator<PutBooksQuery>
    {
        public PutBooksValidator() 
        {
            RuleFor(x => x.categoryIds)
                .NotEmpty()
                .WithMessage("CategoryIds cannot be empty");

            RuleForEach(x => x.categoryIds)
                .GreaterThan(0)
                .WithMessage("Each categoryId must be greater than 0");

            RuleFor(x => x.title)
                .NotEmpty()
                .WithMessage("Please put the title of the book");

            RuleFor(x => x.author)
                .NotEmpty()
                .WithMessage("Please input the author of the book");

            RuleFor(x => x.updatedBy)
                .GreaterThan(0)
                .WithMessage("Please enter Your Id");
        }
    }
}
