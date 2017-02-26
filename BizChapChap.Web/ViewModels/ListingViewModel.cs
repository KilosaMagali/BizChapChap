using BizChapChap.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BizChapChap.Web.ViewModels
{
    [Bind(Exclude = "Photo1, Photo2, Photo3, Photo4, Photo5, Photo6")]
    public class ListingViewModel : IValidatableObject
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateEdit { get; set; }

        public double Price { get; set; }

        public int CurrencyRefId { get; set; }

        public string Description { get; set; }

        public int SubCategoryRefId { get; set; }

        public bool IsPremium { get; set; }

        public string Photo1 { get; set; }

        public string Photo2 { get; set; }

        public string Photo3 { get; set; }

        public string Photo4 { get; set; }

        public string Photo5 { get; set; }

        public string Photo6 { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new ListingViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}