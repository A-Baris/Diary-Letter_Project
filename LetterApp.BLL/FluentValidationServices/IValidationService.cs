using LetterApp.BLL.FluentValidationServices.ModelStateHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.FluentValidationServices
{
    public interface IValidationService<T>
    {
        IEnumerable<ValidationError> GetValidationErrors(T model);
    }
}
