using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Infrastructure.Data;

public static class SeedData
{
    public static async void Initialize(IServiceProvider services)
    {
        await MigrateDatabase(services);
        await CreateUserRoles(services);
        await CreateSuperAdmin(services);
        await CreateDeletedUserAccount(services);
    }

    private static async Task MigrateDatabase(IServiceProvider services)
    {
        var databaseContext = services.GetRequiredService<ApplicationDbContext>();
        await databaseContext.Database.MigrateAsync();
    }

    private static async Task CreateUserRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();

        var superAdminRole = await roleManager.RoleExistsAsync(Role.SuperAdmin.ToString());
        var adminRole = await roleManager.RoleExistsAsync(Role.Admin.ToString());
        var moderatorRole = await roleManager.RoleExistsAsync(Role.Moderator.ToString());
        var editorRole = await roleManager.RoleExistsAsync(Role.Editor.ToString());
        var developerRole = await roleManager.RoleExistsAsync(Role.Developer.ToString());

        if (!superAdminRole)
        {
            await roleManager.CreateAsync(new UserRole(Role.SuperAdmin));
        }
        if (!adminRole)
        {
            await roleManager.CreateAsync(new UserRole(Role.Admin));
        }
        if (!moderatorRole)
        {
            await roleManager.CreateAsync(new UserRole(Role.Moderator));
        }
        if (!editorRole)
        {
            await roleManager.CreateAsync(new UserRole(Role.Editor));
        }
        if (!developerRole)
        {
            await roleManager.CreateAsync(new UserRole(Role.Developer));
        }
    }

    private static async Task CreateSuperAdmin(IServiceProvider service)
    {
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var superAdminUser = await userManager.FindByEmailAsync("suxrobgm@gmail.com");

        if (superAdminUser == null)
        {
            superAdminUser = new ApplicationUser
            {
                UserName = "SuxrobGM",
                Email = "suxrobgm@gmail.com",
                EmailConfirmed = true,
            };
            
            await userManager.CreateAsync(superAdminUser, "Test12345#");
        }

        var hasSuperAdminRole = await userManager.IsInRoleAsync(superAdminUser, Role.SuperAdmin.ToString());

        if (!hasSuperAdminRole)
        {
            await userManager.AddToRoleAsync(superAdminUser, Role.SuperAdmin.ToString());
        }
    }

    private static async Task CreateDeletedUserAccount(IServiceProvider service)
    {
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();

        var deletedUserAccount = await userManager.FindByNameAsync("DELETED_USER");
        if (deletedUserAccount == null)
        {
            deletedUserAccount = new ApplicationUser
            {
                UserName = "DELETED_USER",
                Email = "Test@mail.ru",
                EmailConfirmed = true
            };
            
            await userManager.CreateAsync(deletedUserAccount, "Test12345#");
        }

        var hasSuperAdminRole = await userManager.IsInRoleAsync(deletedUserAccount, Role.SuperAdmin.ToString());

        if (!hasSuperAdminRole)
        {
            await userManager.AddToRoleAsync(deletedUserAccount, Role.SuperAdmin.ToString());
        }
    }
}