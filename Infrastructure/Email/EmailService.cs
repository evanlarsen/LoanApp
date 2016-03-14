using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Infrastructure.Email
{
    public interface IEmailService
    {
        Task Send(string from, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        readonly string to;
        public EmailService(string to)
        {
            this.to = to;
        }
        public async Task Send(string from, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress(from);
            message.To.Add(this.to);
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            await smtpClient.SendMailAsync(message);
        }
    }
}
