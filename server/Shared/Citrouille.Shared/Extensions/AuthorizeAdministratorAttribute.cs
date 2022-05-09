using Microsoft.AspNetCore.Authorization;
using static Citrouille.Shared.Constants;

namespace Citrouille.Shared.Extensions;

public class AuthorizeAdministratorAttribute : AuthorizeAttribute
{
    public AuthorizeAdministratorAttribute() => Roles = AdministratorRoleName;

}