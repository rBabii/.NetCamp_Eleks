using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.UserAggregste.Exceptions
{
    public class UserAddException : Exception
    {
        public UserAddException()
        {

        }
        public UserAddException(string message)
            : base(message)
        {

        }
        public UserAddException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
