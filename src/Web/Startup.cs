using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Syncfusion.Licensing;
using EC_Website.Models.UserModel;
using EC_Website.Data;
using EC_Website.Hubs;
using EC_Website.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EC_Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            SyncfusionLicenseProvider.RegisterLicense(Configuration.GetSection("SynLicenseKey").Value);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("RemoteConnection"))
                    .UseLazyLoadingProxies());

            services.AddDefaultIdentity<User>()
                .AddRoles<UserRole>()
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

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ru-RU")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddTransient<IEmailSender, EmailSender>(_ => new EmailSender(Configuration));
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSignalR();
            services.AddRazorPages()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseRouting();

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RealTimeInteractionHub>("/RealTimeInteractionHub");
                endpoints.MapRazorPages();
            });

            //CreateUserRoles(app.ApplicationServices);
            //AddBadges(app.ApplicationServices);
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

        private void AddBadges(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var superAdmin = db.Users.FirstOrDefault(i => i.UserName == "SuxrobGM");

            var programmer = new Badge() { Name = "Programmer" };
            var scripter = new Badge() { Name = "Scripter" };
           
            superAdmin.UserBadges.Add(new UserBadge() { Badge = programmer });
            superAdmin.UserBadges.Add(new UserBadge() { Badge = scripter });

            db.SaveChanges();
        }
    }
}
