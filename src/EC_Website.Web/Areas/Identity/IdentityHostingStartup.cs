using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EC_Website.Web.Areas.Identity.IdentityHostingStartup))]
namespace EC_Website.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}