using Citrouille.Data.Entities;
using Citrouille.Shared;
using Microsoft.AspNetCore.Identity;

namespace Citrouille.Data.Seed;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityDataSeeder(
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedData()
    {
        if (_roleManager.Roles.Any())
        {
            return;
        }

        Task
            .Run(async () =>
            {
                var adminRole = new IdentityRole(Constants.AdministratorRoleName);

                await _roleManager.CreateAsync(adminRole);

                var adminUser = new User
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    SecurityStamp = "RandomSecurityStamp"
                };

                await _userManager.CreateAsync(adminUser, "adminpass123");

                await _userManager.AddToRoleAsync(adminUser, Constants.AdministratorRoleName);
            })
            .GetAwaiter()
            .GetResult();
    }
}