using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EC_WebSite.Models.UserModel;
using EC_WebSite.Data;
using EC_WebSite.Hubs;

namespace EC_WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies());

            services.AddDefaultIdentity<User>()
                .AddRoles<UserRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789_.-";
                options.User.RequireUniqueEmail = true;
                //options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/ChatHub");
            });

            //CreateUserRoles(serviceProvider);
            //AddSkills(serviceProvider);
        }

        private void CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var superAdminRole = roleManager.RoleExistsAsync(Role.SuperAdmin.ToString()).Result;
            var adminRole = roleManager.RoleExistsAsync(Role.Admin.ToString()).Result;
            var moderatorRole = roleManager.RoleExistsAsync(Role.Moderator.ToString()).Result;
            var developerRole = roleManager.RoleExistsAsync(Role.Developer.ToString()).Result;
            var specialRole = roleManager.RoleExistsAsync(Role.Editor.ToString()).Result;

            if (!superAdminRole)
            {
                var roleResult = roleManager.CreateAsync(new UserRole(Role.SuperAdmin)).Result;
            }
            if (!adminRole)
            {
                var roleResult = roleManager.CreateAsync(new UserRole(Role.Admin)).Result;
            }
            if (!moderatorRole)
            {
                var roleResult = roleManager.CreateAsync(new UserRole(Role.Moderator)).Result;
            }
            if (!developerRole)
            {
                var roleResult = roleManager.CreateAsync(new UserRole(Role.Developer)).Result;
            }
            if (!specialRole)
            {
                var roleResult = roleManager.CreateAsync(new UserRole(Role.Editor)).Result;
            }

            var admin = userManager.FindByEmailAsync("suxrobgm@gmail.com").Result;
            userManager.AddToRoleAsync(admin, Role.SuperAdmin.ToString()).Wait();
            userManager.AddToRoleAsync(admin, Role.Developer.ToString()).Wait();
        }

        private void AddSkills(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var superAdmin = db.Users.Where(i => i.UserName == "SuxrobGM").FirstOrDefault();

            var programmer = new Skill() { Name = "Programmer" };
            var scripter = new Skill() { Name = "Scripter" };
           
            superAdmin.UserSkills.Add(new UserSkill() { Skill = programmer });
            superAdmin.UserSkills.Add(new UserSkill() { Skill = scripter });

            db.SaveChanges();
        }
    }
}
