using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Citrouille.Data.Entities;
using Citrouille.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Citrouille.Infrastructure.Services.Identity;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly ApplicationSettings _applicationSettings;
 
    public TokenGeneratorService(IOptions<ApplicationSettings> applicationSettings) 
        => _applicationSettings = applicationSettings.Value;
    
    public string GenerateToken(User user, IEnumerable<string> roles = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_applicationSettings.Secret);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Id)
        };

        if (roles != null)
        {
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encryptedToken = tokenHandler.WriteToken(token);

        return encryptedToken;
    }
}