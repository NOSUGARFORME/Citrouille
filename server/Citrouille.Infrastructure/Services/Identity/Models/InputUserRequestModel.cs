using System.ComponentModel.DataAnnotations;

namespace Citrouille.Infrastructure.Services.Identity.Models;

public class InputUserRequestModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required] 
    public string Password { get; set; }
}