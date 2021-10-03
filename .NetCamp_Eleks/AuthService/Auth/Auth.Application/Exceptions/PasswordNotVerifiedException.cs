using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions
{
    public class PasswordNotVerifiedException : Exception
    {
        public PasswordNotVerifiedException()
        {

        }
        public PasswordNotVerifiedException(string message)
            : base(message)
        {

        }
        public PasswordNotVerifiedException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
