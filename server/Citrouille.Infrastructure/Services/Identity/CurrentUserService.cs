using System.Security.Claims;
using Citrouille.Shared.Extensions;
using Microsoft.AspNetCore.Http;

namespace Citrouille.Infrastructure.Services.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal _user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User;

        if (_user == null)
        {
            throw new InvalidOperationException("This request does not have an authenticated user.");
        }

        UserId = _user.FindFirstValue(ClaimTypes.Name);
    }

    public string UserId { get; }

    public bool IsAdministrator => _user.IsAdministrator();
}