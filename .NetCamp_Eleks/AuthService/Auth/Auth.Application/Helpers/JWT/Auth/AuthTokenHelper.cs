using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Application.Helpers.JWT.Auth
{
    public class AuthTokenHelper : BaseJwtTokenHelper
    {
        public AuthTokenHelper(IOptions<AuthOptions> authOptions)
            : base(authOptions) { }
    }
}
