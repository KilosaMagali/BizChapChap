using BizChapChap.Web.Infrastructure.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BizChapChap.Web.ViewModels
{
    [Bind(Exclude = "Id")]
    public class CategoryViewModel : IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new CategoryViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}