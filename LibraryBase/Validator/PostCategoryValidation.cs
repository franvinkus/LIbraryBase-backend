using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class PostCategoryValidation : AbstractValidator<PostCategoryQuery>
    {
        public PostCategoryValidation() 
        {
            RuleFor(x => x.categoryName)
                .NotEmpty()
                .WithMessage("Category name cannot be empty");

            RuleFor(x => x.createdBy)
                .GreaterThan(0);
        }
    }
}
