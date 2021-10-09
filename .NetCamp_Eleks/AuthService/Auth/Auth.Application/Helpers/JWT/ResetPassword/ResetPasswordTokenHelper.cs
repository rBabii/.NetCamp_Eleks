using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Application.Helpers.JWT.ResetPassword
{
    public class ResetPasswordTokenHelper : BaseJwtTokenHelper
    {
        public ResetPasswordTokenHelper(IOptions<ResetPasswordTokenOptions> resetPasswordTokenOptions) 
            : base(resetPasswordTokenOptions) { }
    }
}
