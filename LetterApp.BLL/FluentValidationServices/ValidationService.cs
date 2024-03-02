using FluentValidation;
using LetterApp.BLL.FluentValidationServices.ModelStateHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LetterApp.BLL.FluentValidationServices
{
    public class ValidationService<T>:IValidationService<T>
    {
        private readonly IValidator<T> _validator;

        public ValidationService(IValidator<T> validator)
        {
            _validator = validator;
        }
        public IEnumerable<ValidationError> GetValidationErrors(T model)
        {
            var validationResult = _validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(failure => new ValidationError
                {
                    PropertyName = failure.PropertyName,
                    ErrorMessage = failure.ErrorMessage
                });
            }

            return Enumerable.Empty<ValidationError>();
        }

    }
}
