using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPlatform.Application.Managers.EmailManager.Params;
using BlogPlatform.Application.Managers.EmailManager.Result;
using BlogPlatform.Infrastructure.HttpServices.Email;
using External.Result.Helpers;

namespace BlogPlatform.Application.Managers.EmailManager
{
    public class EmailManager
    {
        private readonly EmailService _emailService;
        public EmailManager(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<SendEmailResult> Send(SendEmailParams sendEmailParams)
        {
            var validationResult = ParamsValidator.Validate(sendEmailParams);
            if (validationResult != null)
            {
                return new SendEmailResult(validationResult);
            }

            var result = await _emailService.Send(new External.DTOs.EmailService.Models.Request.SendEmailRequest() 
            {
                EmailAddressesFrom = sendEmailParams.EmailAddressesFrom
                .Select(EAF => new External.DTOs.EmailService.Models.Request.EmailAddress() 
                {
                    Name = EAF.Name,
                    Address = EAF.Address
                }),
                EmailAddressesTo = sendEmailParams.EmailAddressesTo
                .Select(EAT => new External.DTOs.EmailService.Models.Request.EmailAddress()
                {
                    Name = EAT.Name,
                    Address = EAT.Address
                }),
                Body = sendEmailParams.Body,
                Subject = sendEmailParams.Subject 
            });

            if (!result.IsSuccess)
            {
                return new SendEmailResult(new External.Result.Base.Error(result.Error.ErrorMessages, (External.Result.Base.ErrorType)result.Error.ErrorType));
            }
            return new SendEmailResult();
        }
    }
}
