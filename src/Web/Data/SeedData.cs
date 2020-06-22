using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using EC_Website.Models.UserModel;

namespace EC_Website.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            CreateUserRoles(serviceProvider);
        }

        private static async void CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var superAdminRole = await roleManager.RoleExistsAsync(Role.SuperAdmin.ToString());
            var adminRole = await roleManager.RoleExistsAsync(Role.Admin.ToString());
            var moderatorRole = await roleManager.RoleExistsAsync(Role.Moderator.ToString());
            var editorRole = await roleManager.RoleExistsAsync(Role.Editor.ToString());
            var developerRole = roleManager.RoleExistsAsync(Role.Developer.ToString()).Result;

            if (!superAdminRole)
            {
                var roleResult = await roleManager.CreateAsync(new UserRole(Role.SuperAdmin));
            }
            if (!adminRole)
            {
                var roleResult = await roleManager.CreateAsync(new UserRole(Role.Admin));
            }
            if (!moderatorRole)
            {
                var roleResult = await roleManager.CreateAsync(new UserRole(Role.Moderator));
            }
            if (!developerRole)
            {
                var roleResult = await roleManager.CreateAsync(new UserRole(Role.Developer));
            }
            if (!editorRole)
            {
                var roleResult = await roleManager.CreateAsync(new UserRole(Role.Editor));
            }

            var admin = await userManager.FindByEmailAsync("suxrobgm@gmail.com");

            if (admin != null)
            {
                var hasSuperAdminRole = await userManager.IsInRoleAsync(admin, Role.SuperAdmin.ToString());
                var hasDeveloperRole = await userManager.IsInRoleAsync(admin, Role.Developer.ToString());

                if (!hasSuperAdminRole)
                {
                    await userManager.AddToRoleAsync(admin, Role.SuperAdmin.ToString());
                }

                if (!hasDeveloperRole)
                {
                    await userManager.AddToRoleAsync(admin, Role.Developer.ToString());
                }
            }
        }
    }
}
