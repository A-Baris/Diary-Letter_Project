using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LetterApp.BLL.FluentValidationServices.ModelStateHelper
{
    public  class ValidationHelper
    {
        public static ModelStateDictionary HandleValidationErrors(IEnumerable<ValidationError> errors)
        {
            var modelState = new ModelStateDictionary();
            

            foreach (var error in errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return modelState;
        }
    }
}
