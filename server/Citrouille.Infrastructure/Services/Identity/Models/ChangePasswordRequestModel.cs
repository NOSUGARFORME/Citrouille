namespace Citrouille.Infrastructure.Services.Identity.Models;

public class ChangePasswordRequestModel
{
    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
}