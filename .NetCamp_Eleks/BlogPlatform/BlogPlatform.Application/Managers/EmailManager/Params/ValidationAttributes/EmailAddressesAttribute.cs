using BlogPlatform.Application.Managers.EmailManager.Params;
using External.Result.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Application.Managers.EmailManager.Params.ValidationAttributes
{
    public class EmailAddressesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var addresses = value as IEnumerable<EmailAddress>;
                foreach (var address in addresses)
                {
                    var validationResult = ParamsValidator.Validate(address);
                    if (validationResult != null)
                    {
                        ErrorMessage = string.Join("; ", validationResult.ErrorMessages);
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
