using EmailService.Domain.AgregatesModel.EmailAggregate;
using External.Options.EmailService;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.Managers.EmailManager.Extentions
{
    public static class EmailExtention
    {
        public static async Task Send(this Email email, EmailServiceOptions options)
        {
            var mimeMessage = new MimeMessage();
            foreach (var emailAddress in email.EmailAddressesTo)
            {
                mimeMessage.To.Add(new MailboxAddress(emailAddress.Name, emailAddress.Address));
            }
            foreach (var emailAddress in email.EmailAddressesFrom)
            {
                mimeMessage.From.Add(new MailboxAddress(emailAddress.Name, emailAddress.Address));
            }
            mimeMessage.Subject = email.Subject;

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = email.Body;

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Prepare(EncodingConstraint.EightBit);
            if (options.UseAuth)
            {
                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(options.SmtpServer, options.PortNumber,
                        options.UseSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None);
                    await emailClient.AuthenticateAsync(options.SmtpUserName, options.SmtpPassword);
                    await emailClient.SendAsync(mimeMessage);
                }
            }
            else
            {
                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(options.SmtpServer, options.PortNumber,
                        options.UseSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None);
                    await emailClient.SendAsync(mimeMessage);
                }
            }
        }
    }
}
