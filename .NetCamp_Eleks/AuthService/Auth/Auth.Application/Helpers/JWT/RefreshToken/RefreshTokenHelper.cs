using External.Options.Auth;
using Microsoft.Extensions.Options;

namespace Auth.Application.Helpers.JWT.RefreshToken
{
    public class RefreshTokenHelper : BaseJwtTokenHelper
    {

        public RefreshTokenHelper(IOptions<RefreshOptions> refreshOptions)
            : base(refreshOptions) { }
    }
}
