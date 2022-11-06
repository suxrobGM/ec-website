using System.Globalization;
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
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Core.Interfaces.Services;
using EC_Website.Infrastructure.Data;
using EC_Website.Infrastructure.Repositories;
using EC_Website.Infrastructure.Services;
using EC_Website.Web.Authorization;
using EC_Website.Web.Hubs;
using EC_Website.Web.Resources;
using EC_Website.Web.Utils;

namespace EC_Website.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // SyncfusionLicenseProvider.RegisterLicense(Configuration.GetSection("SynLicenseKey").Value);

        // Infrastructure layer
        ConfigureDatabases(services);
        services.AddSingleton<RealTimeDataContext>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IForumRepository, ForumRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWikiRepository, WikiRepository>();

        // Web layer
        ConfigureIdentity(services);
        ConfigureLocalization(services);

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = _ => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.HasAdminRole, builder =>
            {
                builder.RequireRole("SuperAdmin", "Admin");
            });
            options.AddPolicy(Policies.CanManageBlogs, builder =>
            {
                builder.RequireRole("SuperAdmin", "Admin", "Editor");
            });
            options.AddPolicy(Policies.CanManageWikiPages, builder =>
            {
                builder.RequireRole("SuperAdmin", "Admin", "Editor");
            });
            options.AddPolicy(Policies.CanManageForums, builder =>
            {
                builder.RequireRole("SuperAdmin", "Admin", "Moderator");
            });
            options.AddPolicy(Policies.CanBanUsers, builder =>
            {
                builder.RequireRole("SuperAdmin", "Admin", "Moderator");
            });
        });

        services.AddScoped<ImageHelper>();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddSignalR();
        services.AddRazorPages()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => 
                    factory.Create(typeof(SharedResource));
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) 
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");                
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        var localizationOptions = app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
        app.UseRequestLocalization(localizationOptions);

        app.UseCookiePolicy();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ApplicationHub>("/ApplicationHub");
            endpoints.MapRazorPages();
        });
    }

    private void ConfigureDatabases(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                    Configuration.GetConnectionString("RemoteDatabase")!)
                .UseLazyLoadingProxies());
    }

    private void ConfigureIdentity(IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789_.-";
            options.User.RequireUniqueEmail = true;
            //options.SignIn.RequireConfirmedAccount = true;
        });

        // services.AddAuthentication()
        //     .AddGoogle(options =>
        //     {
        //         var googleAuthNSection = Configuration.GetSection("Authentication:Google");
        //         options.ClientId = googleAuthNSection["ClientId"];
        //         options.ClientSecret = googleAuthNSection["ClientSecret"];
        //     })
        //     .AddFacebook(options =>
        //     {
        //         var facebookAuthSection = Configuration.GetSection("Authentication:Facebook");
        //         options.AppId = facebookAuthSection["AppId"];
        //         options.AppSecret = facebookAuthSection["AppSecret"];
        //     });
    }

    private static void ConfigureLocalization(IServiceCollection services)
    {
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

        services.AddLocalization(options => options.ResourcesPath = "Resources");
    }
}