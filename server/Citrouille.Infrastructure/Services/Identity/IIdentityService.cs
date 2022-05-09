using Citrouille.Data.Entities;
using Citrouille.Infrastructure.Services.Identity.Models;
using Citrouille.Shared.Services;

namespace Citrouille.Infrastructure.Services.Identity;

public interface IIdentityService
{
    Task<Result<User>> Register(InputUserRequestModel model);
    Task<Result<UserOutputModel>> Login(InputUserRequestModel model);
    Task<Result> ChangePassword(string userId, ChangePasswordRequestModel model);
    Task<Result> ChangeAdminRole(ChangeAdminRoleRequestModel model);
}