using Citrouille.Infrastructure.Services.Identity;
using Citrouille.Infrastructure.Services.Identity.Models;
using Citrouille.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Citrouille.Api.Controllers;

public class IdentityController : BaseController
{
    private readonly IIdentityService _identity;
    private readonly ICurrentUserService _currentUser;

    public IdentityController(
        IIdentityService identity, 
        ICurrentUserService currentUser)
    {
        _identity = identity;
        _currentUser = currentUser;
    }
    
    [HttpPost]
    [Route(nameof(Register))]
    public async Task<ActionResult<UserOutputModel>> Register([FromBody] InputUserRequestModel input)
    {
        var result = await _identity.Register(input);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return await Login(input);
    }
    
    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<UserOutputModel>> Login([FromBody] InputUserRequestModel input)
    {
        var result = await _identity.Login(input);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return new UserOutputModel(result.Data.Token);
    }
    
    [HttpPut]
    [Authorize]
    [Route(nameof(ChangePassword))]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequestModel input)
        => await _identity.ChangePassword(_currentUser.UserId, new ChangePasswordRequestModel
        {
            CurrentPassword = input.CurrentPassword,
            NewPassword = input.NewPassword
        });

    [HttpPut]
    [AuthorizeAdministrator]
    [Route(nameof(ChangeAdminRole))]
    public async Task<ActionResult> ChangeAdminRole([FromBody] ChangeAdminRoleRequestModel model)
        => await _identity.ChangeAdminRole(model);
}
