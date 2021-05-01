using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UsedGamesAPI.Models;
using UsedGamesAPI.Models.Enums;

namespace UsedGamesAPI.Services.Token
{
    public static class TokenService
    {
        public static string GenerateToken(User user, AccountType accountType = AccountType.Client)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, accountType.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(GetKey()), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static byte[] GetKey() => Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("UsedGamesAPISQL_Secret", EnvironmentVariableTarget.User));
    }
}
