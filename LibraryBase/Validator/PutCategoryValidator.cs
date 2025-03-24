using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class PutCategoryValidator : AbstractValidator<PutCategoryQuery>
    {
        public PutCategoryValidator() 
        {

            RuleFor(x => x.categoryName)
                .NotEmpty()
                .WithMessage("This field must be filled");
        }
    }
}
