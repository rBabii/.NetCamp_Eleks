using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.Result.Base
{
    public class Error
    {
        public IEnumerable<string> ErrorMessages { get; set; }
        public ErrorType ErrorType { get; set; }

        public Error(string errorMessage, ErrorType errorType) : this(new List<string>() { errorMessage }, errorType) { }
        public Error(IEnumerable<string> errorMessages, ErrorType errorType)
        {
            ErrorType = errorType;
            ErrorMessages = errorMessages;
        }
    }
}
