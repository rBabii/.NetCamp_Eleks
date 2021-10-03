using Auth.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Helpers
{
    public class RefreshTokenValidator
    {
        private readonly IOptions<RefreshOptions> refreshOptions;

        public RefreshTokenValidator(IOptions<RefreshOptions> refreshOptions)
        {
            this.refreshOptions = refreshOptions;
        }

        public bool Validate(string refreshToken)
        {
            var options = refreshOptions.Value;
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = options.Issuer,

                ValidateAudience = true,
                ValidAudience = options.Audience,

                ValidateLifetime = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret)),
                ValidateIssuerSigningKey = true
            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                jwtSecurityTokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
