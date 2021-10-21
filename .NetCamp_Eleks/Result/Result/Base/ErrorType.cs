using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Result.Base
{
    public enum ErrorType
    {
        None = 0,
        Info = 1,
        Validation = 2,
        SystemError = 3,
        Exception = 4
    }
}
