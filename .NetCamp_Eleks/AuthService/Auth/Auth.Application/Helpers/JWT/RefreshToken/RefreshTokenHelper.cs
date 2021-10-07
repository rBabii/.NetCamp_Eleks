using Auth.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Helpers.JWT.RefreshToken
{
    public class RefreshTokenHelper : BaseJwtOptionsCtor
    {

        public RefreshTokenHelper(IOptions<RefreshOptions> refreshOptions)
            : base(refreshOptions) { }

        public string GenerateJWT()
        {
            var refreshParams = _jwtOptions.Value;
            return TokenGenerator.GenerateJWT(
                    refreshParams.Secret,
                    refreshParams.Issuer,
                    refreshParams.Audience,
                    refreshParams.TokenLifetime
                    );
        }

        public bool Validate(string refreshToken)
        {
            var options = _jwtOptions.Value;
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
