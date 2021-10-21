using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Result.Base
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
        public bool CanContinue
        {
            get
            {
                return Error == null || Error.ErrorType == ErrorType.Info;
            }
        }
        public BaseResult(Error error)
        {
            Error = error;
        }
    }
}
