using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions
{
    public class RefreshTokenNotValidException : Exception
    {
        public RefreshTokenNotValidException()
        {

        }
        public RefreshTokenNotValidException(string message)
            : base(message)
        {

        }
        public RefreshTokenNotValidException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
