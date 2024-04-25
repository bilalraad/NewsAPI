using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NewsAPI.Interfaces;

namespace NewsAPI.Services
{
    public class TokenService : ITokenService
    {
        // Symmetric means a key that's only dependent on secret key
        // no public keys need to be sent/used
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["secretKey"] ?? "YouShouldChangeThis"));
        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),

                };
            var tokenDescriptor = new SecurityTokenDescriptor

            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}