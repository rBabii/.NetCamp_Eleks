using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions
{
    public class UserRegisterException : Exception
    {
        public UserRegisterException()
        {

        }
        public UserRegisterException(string message)
            : base(message)
        {

        }
        public UserRegisterException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
