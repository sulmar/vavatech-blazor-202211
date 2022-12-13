using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vavatech.AuthApi.Domain;

namespace Vavatech.AuthApi.Infrastructure;


// dotnet add package System.IdentityModel.Tokens.Jwt
public class JwtTokenService : ITokenService
{
    public string Create(User user)
    {
        string secretKey = "your-256-bit-secret";

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        string issuer = "shopper.vavatech.pl";
        string audience = "domain.com";

        var securityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials            
            );

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken );

        return token;
    }
}
