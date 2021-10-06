using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Result
{
    public abstract class BaseResult
    {
        public Error Error { get; set; }
        public bool IsValid
        { 
            get 
            {
                return Error == null;
            }
        }
        public BaseResult(Error error)
        {
            Error = error;
        }
    }
}
