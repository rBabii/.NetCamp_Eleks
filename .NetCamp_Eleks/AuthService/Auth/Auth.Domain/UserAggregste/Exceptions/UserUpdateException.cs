using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.UserAggregste.Exceptions
{
    public class UserUpdateException : Exception
    {
        public UserUpdateException()
        {

        }
        public UserUpdateException(string message)
            : base(message)
        {

        }
        public UserUpdateException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
