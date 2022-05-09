using System.Security.Claims;
using static Citrouille.Shared.Constants;

namespace Citrouille.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAdministrator(this ClaimsPrincipal user)
        => user.IsInRole(AdministratorRoleName);
}