using BizChapChap.Web.Infrastructure.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BizChapChap.Web.ViewModels
{
    public class SubCategoryViewModel : IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryRefId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new SubCategoryViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}