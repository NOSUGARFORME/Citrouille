using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Services.Identity;

public interface ITokenGeneratorService
{
    string GenerateToken(User user, IEnumerable<string> roles = null);
}