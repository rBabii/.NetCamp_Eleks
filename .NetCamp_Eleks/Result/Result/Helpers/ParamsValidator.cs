using Result.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Result.Helpers
{
    public static class ParamsValidator
    {
        public static Error Validate<T>(T objectParam) where T : class
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(objectParam);
            if (!Validator.TryValidateObject(objectParam, context, results, true))
            {
                IEnumerable<string> ErrorMessages = results.Select(v => v.ErrorMessage);
                return new Error(ErrorMessages, ErrorType.Validation);
            }
            return null;
        }
    }
}
