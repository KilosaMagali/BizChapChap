using BizChapChap.Web.ViewModels;
using FluentValidation;

namespace BizChapChap.Web.Infrastructure.Validators
{
    public class SubCategoryViewModelValidator : AbstractValidator<SubCategoryViewModel>
    {
        public SubCategoryViewModelValidator()
        {
            RuleFor(subCategory => subCategory.Name).NotEmpty()
               .WithMessage("The category name is required");
            RuleFor(subCategory => subCategory.CategoryRefId).GreaterThan(0)
                .WithMessage("Select a category for this subcategory");
        }
    }
}