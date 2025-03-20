using FluentValidation;
using LibraryBase.Query;

namespace LibraryBase.Validator
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryQuery>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x => x.deletedCateId)
                .GreaterThan(0)
                .WithMessage("Input valid Id");
        }
    }
}
