using System.ComponentModel.DataAnnotations;

namespace Citrouille.Infrastructure.Services.Identity.Models;

public class LoginUserRequestModel
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}