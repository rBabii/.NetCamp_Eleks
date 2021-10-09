using Auth.Infrastructure.Services.Email.Models;
using Auth.Infrastructure.Services.Email.Options;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MailKit;
using MailKit.Security;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Services.Email
{
    public class EmailService
    {
        private readonly IOptions<EmailServiceOptions> _emailServiceOptions;
        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions)
        {
            _emailServiceOptions = emailServiceOptions;
        }
        public void SendEmail(SendEmailModel sendEmailModel)
        {
            var options = _emailServiceOptions.Value;
            try
            {
                var mimeMessage = new MimeMessage();
                foreach(var emailAddress in sendEmailModel.EmailAddressesTo)
                {
                    mimeMessage.To.Add(new MailboxAddress(emailAddress.Name, emailAddress.Address));
                }
                foreach (var emailAddress in sendEmailModel.EmailAddressesFrom)
                {
                    mimeMessage.From.Add(new MailboxAddress(emailAddress.Name, emailAddress.Address));
                }
                mimeMessage.Subject = sendEmailModel.Subject;

                var bodyBuilder = new BodyBuilder();

                bodyBuilder.HtmlBody = sendEmailModel.Body;

                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Prepare(EncodingConstraint.EightBit);
                if (options.UseAuth)
                {
                    using (var emailClient = new SmtpClient())
                    {
                        emailClient.Connect(options.SmtpServer, options.PortNumber, 
                            options.UseSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None);
                        emailClient.Authenticate(options.SmtpUserName, options.SmtpPassword);
                        emailClient.Send(mimeMessage);
                    }
                }
                else
                {
                    using (var emailClient = new SmtpClient())
                    {
                        emailClient.Connect(options.SmtpServer, options.PortNumber, 
                            options.UseSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None);
                        emailClient.Send(mimeMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Add logs
            }
        }
    }
}
