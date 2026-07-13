using E_Commerce01.Domain.Contract;
using E_Commerce01.Domain.Identity;
using E_Commerce01.Infrastructure.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Infrastructure.DataSeeding
{
    public class IdentityDataSeeder(
        StoreIdentityDbContext context,
        ILogger<StoreIdentityDbContext> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigration = await context.Database.GetPendingMigrationsAsync(ct);
                if(pendingMigration.Any())
                {
                    await context.Database.MigrateAsync(ct);
                }

                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!await userManager.Users.AnyAsync())
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "admin",
                        UserName = "admin",
                        Email = "admin@system.com",
                        PhoneNumber = "01287566847"
                    };

                    var result = await userManager.CreateAsync(admin , "P@ssW0rd");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            logger.LogWarning(item.ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
