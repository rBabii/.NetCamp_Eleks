using Auth.Application.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Helpers.JWT
{
    public abstract class BaseJwtOptionsCtor
    {
        protected IOptions<BaseJwtOptions> _jwtOptions;
        public BaseJwtOptionsCtor(IOptions<BaseJwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
    }
}
