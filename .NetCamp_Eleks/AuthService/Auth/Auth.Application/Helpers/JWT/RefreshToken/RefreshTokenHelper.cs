using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Application.Helpers.JWT.RefreshToken
{
    public class RefreshTokenHelper : BaseJwtTokenHelper
    {

        public RefreshTokenHelper(IOptions<RefreshOptions> refreshOptions)
            : base(refreshOptions) { }
    }
}
