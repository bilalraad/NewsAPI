using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public string CreateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
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



        public (byte[], byte[]) GenerateHash(string password, byte[]? salt)
        {

            using HMACSHA512 hmac = salt != null ? new HMACSHA512(salt) : new HMACSHA512();
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (computedHash, hmac.Key);
        }
    }
}