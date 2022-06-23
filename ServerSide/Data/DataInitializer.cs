/*using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class SeedDataApplicationRoles
{
    public static void SeedAspNetRoles(RoleManager<IdentityRole> roleManager)
    {
        List<string> roleList = new List<string>()
            {
                "Admin",
                "SuperAdmin",
                "User",
                "Supervisor"
            };

        foreach (var role in roleList)
        {
            var result = roleManager.RoleExistsAsync(role).Result;
            if (!result)
            {
                roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}*/