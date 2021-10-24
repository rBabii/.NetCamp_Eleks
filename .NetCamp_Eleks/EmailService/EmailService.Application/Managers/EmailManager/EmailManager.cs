using EmailService.Domain.AgregatesModel.EmailAggregate;
using External.Options.EmailService;
using Microsoft.Extensions.Options;
using EmailService.Application.Managers.EmailManager.Extentions;
using System;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Security;
using MailKit.Net.Smtp;
using MailKit;
using EmailService.Application.Managers.EmailManager.Result;
using EmailService.Application.Managers.EmailManager.Params;
using External.Result.Helpers;
using External.Result.Base;

namespace EmailService.Application.Managers.EmailManager
{
    public class EmailManager
    {
        private readonly IOptions<EmailServiceOptions> _emailServiceOptions;
        public EmailManager(IOptions<EmailServiceOptions> emailServiceOptions)
        {
            _emailServiceOptions = emailServiceOptions;
        }

        public async Task<SendEmailResult> SendAsync(SendEmailParams sendEmailParams)
        {
            var validationResult = ParamsValidator.Validate(sendEmailParams);
            if(validationResult != null)
            {
                return new SendEmailResult(validationResult);
            }

            var email = new Email() 
            {
                EmailAddressesFrom = sendEmailParams.EmailAddressesFrom
                .Select(EAF => new Domain.AgregatesModel.EmailAggregate.EmailAddress() 
                {
                    Address = EAF.Address,
                    Name = EAF.Name
                }),
                EmailAddressesTo = sendEmailParams.EmailAddressesTo
                .Select(EAT => new Domain.AgregatesModel.EmailAggregate.EmailAddress()
                {
                    Address = EAT.Address,
                    Name = EAT.Name
                }),
                Body = sendEmailParams.Body,
                Subject = sendEmailParams.Subject
            };
            try
            {
                await email.Send(_emailServiceOptions.Value);
            }
            catch (SslHandshakeException ex)
            {
                return new SendEmailResult(new Error(ex.Message, ErrorType.Exception));
            }
            catch (SmtpProtocolException ex)
            {
                return new SendEmailResult(new Error(ex.Message, ErrorType.Exception));
            }
            catch (AuthenticationException ex)
            {
                return new SendEmailResult(new Error(ex.Message, ErrorType.Exception));
            }
            catch (ServiceNotConnectedException ex)
            {
                return new SendEmailResult(new Error(ex.Message, ErrorType.Exception));
            }
            catch (ServiceNotAuthenticatedException ex)
            {
                return new SendEmailResult(new Error(ex.Message, ErrorType.Exception));
            }
            catch (ProtocolException ex)
            {
                return new SendEmailResult(new Error(ex.Message, ErrorType.Exception));
            }
            catch (Exception ex)
            {
                return new SendEmailResult(new Error(ex.Message, ErrorType.Exception));
            }
            
            return new SendEmailResult();
        }
    }
}
