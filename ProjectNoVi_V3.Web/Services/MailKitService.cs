using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using NETCore.MailKit.Infrastructure.Internal;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;


//
// User word WEL toegevoegd aan database zonder mail validatie 
//
// Heeft gewerkt maar zorgt nu voor een SocketException
// "SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.
// Vermoed dat dit aan de verbinding met de School Mail Service ligt
// 


namespace ProjectNoVi_V3.Web.Services
{
    public class MailKitService : IEmailSender
    {
        public MailKitService(IOptions<MailKitOptions> options)
        {
            this.Options = options.Value;
        }
        public MailKitOptions Options { get; set; }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(email, subject, message);
        }
        public Task Execute(string to, string subject, string message)
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(Options.SenderEmail);
            if (!string.IsNullOrEmpty(Options.SenderName))
                email.Sender.Name = Options.SenderName;
            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };
            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(Options.Server, Options.Port, Options.Security);
                smtp.Authenticate(Options.Account, Options.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            return Task.FromResult(true);
        }
    }
}

