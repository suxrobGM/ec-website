using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace EC_Website.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.Run(() =>
            {
                const string from = "noreply@ecrisis.su";
                var pass = _configuration.GetSection("NoReplyPassword").Value;
                using var message = new MailMessage(from, email)
                {
                    Subject = subject, 
                    Body = htmlMessage, 
                    IsBodyHtml = true
                };

                using var sc = new SmtpClient("mail5013.site4now.net", 25)
                {
                    EnableSsl = false,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from, pass)
                };
                sc.Send(message);
            });
        }
    }
}
