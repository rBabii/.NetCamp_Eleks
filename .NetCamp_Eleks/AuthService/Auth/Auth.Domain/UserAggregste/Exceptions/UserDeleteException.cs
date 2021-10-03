using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.UserAggregste.Exceptions
{
    public class UserDeleteException : Exception
    {
        public UserDeleteException()
        {

        }
        public UserDeleteException(string message)
            : base(message)
        {

        }
        public UserDeleteException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
