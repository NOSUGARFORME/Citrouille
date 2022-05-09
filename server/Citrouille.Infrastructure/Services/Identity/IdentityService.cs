using Citrouille.Data.Entities;
using Citrouille.Infrastructure.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Citrouille.Shared.Services;
using static Citrouille.Shared.Constants;

namespace Citrouille.Infrastructure.Services.Identity;

public class IdentityService : IIdentityService
{
    private const string InvalidErrorMessage = "Invalid credentials.";
    
    private readonly UserManager<User> _userManager;
    private readonly ITokenGeneratorService _jwtTokenGenerator;

    public IdentityService(
        UserManager<User> userManager,
        ITokenGeneratorService jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<User>> Register(InputUserRequestModel model)
    {
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email
        };
        
        var identityResult = await _userManager.CreateAsync(user, model.Password);

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result<User>.SuccessWith(user)
            : Result<User>.Failure(errors);
    }

    public async Task<Result<UserOutputModel>> Login(InputUserRequestModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!passwordValid)
        {
            return InvalidErrorMessage;
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return new UserOutputModel(token);
    }

    public async Task<Result> ChangePassword(string userId, ChangePasswordRequestModel model)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var identityResult = await _userManager.ChangePasswordAsync(
            user,
            model.CurrentPassword,
            model.NewPassword);

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(errors);
    }

    public async Task<Result> ChangeAdminRole(ChangeAdminRoleRequestModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        IdentityResult identityResult;
        if (await _userManager.IsInRoleAsync(user, AdministratorRoleName))
        {
            identityResult = await _userManager.RemoveFromRoleAsync(user, AdministratorRoleName);
        }
        else
        {
            identityResult = await _userManager.AddToRoleAsync(user, AdministratorRoleName);
        }
        
        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(errors);
    }
}