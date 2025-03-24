using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class PostCategoryValidator : AbstractValidator<PostCategoryQuery>
    {
        public PostCategoryValidator() 
        {
            RuleFor(x => x.categoryName)
                .NotEmpty()
                .WithMessage("Category name cannot be empty");

        }
    }
}
