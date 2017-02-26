using BizChapChap.Web.ViewModels;
using FluentValidation;

namespace BizChapChap.Web.Infrastructure.Validators
{
    public class ListingViewModelValidator : AbstractValidator<ListingViewModel>
    {
        public ListingViewModelValidator()
        {
            RuleFor(listing => listing.Price).GreaterThan(0)
                .WithMessage("Add the price of your item");
            RuleFor(listing => listing.CurrencyRefId).GreaterThan(0)
                .WithMessage("Select Currency");

            RuleFor(listing => listing.SubCategoryRefId).GreaterThan(0)
               .WithMessage("Select a Category");

            RuleFor(listing => listing.Description).NotEmpty()
                .WithMessage("Add a short description");

        }
    }
}