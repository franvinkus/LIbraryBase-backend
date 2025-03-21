using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class PostBooksValidator : AbstractValidator<PostBooksQuery>
    {
        public PostBooksValidator() 
        {
            RuleFor(x => x.cateId)
                .GreaterThan(0)
                .WithMessage("Id starts from 1");

            RuleFor(x => x.title)
                .NotEmpty()
                .WithMessage("Please put the title of the book");

            RuleFor(x => x.author)
                .NotEmpty()
                .WithMessage("Please input the author of the book");
        }
    }
}
