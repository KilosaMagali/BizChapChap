using BizChapChap.Web.ViewModels;
using FluentValidation;

namespace BizChapChap.Web.Infrastructure.Validators
{
    public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(category => category.Name).NotEmpty()
               .WithMessage("The category name is required");
        }
    }
}