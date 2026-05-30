using MimeKit;
using MailKit.Net.Smtp;
using StudentManager.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StudentManager.API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body, string attachmentPath = null)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Student Manager", _configuration["EmailSettings:FromEmail"]));
            emailMessage.To.Add(new MailboxAddress("", toEmail));

            emailMessage.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            var bodyPart = new TextPart("html") { Text = body };
            bodyBuilder.HtmlBody = body;

            if (!string.IsNullOrEmpty(attachmentPath) && System.IO.File.Exists(attachmentPath))
            {
                bodyBuilder.Attachments.Add(attachmentPath);
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:AppPassword"]);
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);







        }
    }
}
