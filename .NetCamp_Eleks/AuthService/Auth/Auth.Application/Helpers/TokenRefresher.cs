using Auth.Application.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Helpers
{
    public class TokenRefresher
    {
        private readonly IOptions<RefreshOptions> refreshOptions;

        public TokenRefresher(IOptions<RefreshOptions> refreshOptions)
        {
            this.refreshOptions = refreshOptions;
        }

        public string GenerateJWT()
        {
            var refreshParams = refreshOptions.Value;
            return  TokenGenerator.GenerateJWT(
                    refreshParams.Secret,
                    refreshParams.Issuer,
                    refreshParams.Audience,
                    refreshParams.TokenLifetime
                    );
        }
    }
}
