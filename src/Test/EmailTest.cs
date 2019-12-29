using System.IO;
using Microsoft.Extensions.Configuration;
using Xunit;
using EC_Website.Services;

namespace EC_Website.UnitTest
{
    public class EmailTest
    {
        [Fact]
        public async void TestSenderMethod()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var sender = new EmailSender(configuration);
            await sender.SendEmailAsync("suxrobGM@gmail.com", "Test", "Test Email");
        }
    }
}
