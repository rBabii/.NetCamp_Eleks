using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Result
{
    public class Error
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public Error(string errorMessage) : this(new List<string>() { errorMessage }) { }
        public Error(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
