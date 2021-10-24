using EmailService.Application.Managers.EmailManager;
using EmailService.Application.Managers.EmailManager.Params;
using External.DTOs.Common.Enums;
using External.DTOs.Common.Models;
using External.DTOs.EmailService.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailManager _emailManager;
        public EmailController(EmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        [HttpPost]
        public async Task<IActionResult> Send(SendEmailRequest sendEmailRequest)
        {
            var result = await _emailManager.SendAsync(new SendEmailParams() 
            {
                EmailAddressesFrom = sendEmailRequest.EmailAddressesFrom
                .Select(EAF => new Application.Managers.EmailManager.Params.EmailAddress() 
                {
                    Address = EAF.Address,
                    Name = EAF.Name
                }),
                EmailAddressesTo = sendEmailRequest.EmailAddressesTo
                .Select(EAT => new Application.Managers.EmailManager.Params.EmailAddress()
                {
                    Address = EAT.Address,
                    Name = EAT.Name
                }),
                Body = sendEmailRequest.Body,
                Subject = sendEmailRequest.Subject
            });

            if (!result.IsValid && !result.CanContinue)
            {
                return BadRequest(new Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (ErrorType)result.Error.ErrorType
                });
            }

            return Ok();
        }
    }
}
