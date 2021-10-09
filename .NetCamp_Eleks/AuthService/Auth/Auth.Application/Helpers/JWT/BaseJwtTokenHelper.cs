using Auth.Application.Options;
using Auth.Domain.UserAggregste;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Helpers.JWT
{
    public abstract class BaseJwtTokenHelper
    {
        protected readonly IOptions<BaseJwtOptions> _jwtOptions;
        public BaseJwtTokenHelper(IOptions<BaseJwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string GenerateJWT(User user)
        {
            var options = _jwtOptions.Value;
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(CustomClaims.Role, role.Name.ToString()));
            }

            return TokenGenerator.GenerateJWT(
                    options.Secret,
                    options.Issuer,
                    options.Audience,
                    options.TokenLifetime,
                    claims);
        }
        public JwtSecurityToken Validate(string tokenString)
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
                jwtSecurityTokenHandler.ValidateToken(tokenString, tokenValidationParameters, out SecurityToken validatedToken);
                var token = jwtSecurityTokenHandler.ReadJwtToken(tokenString);
                return token;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
