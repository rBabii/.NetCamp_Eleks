using BlogPlatform.Application.Managers.EmailManager.Params.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.EmailManager.Params
{
    public class SendEmailParams
    {
        [Required(ErrorMessage = "EmailAddressesTo is Reqired.")]
        [EmailAddresses]
        public IEnumerable<EmailAddress> EmailAddressesTo { get; set; }

        [Required(ErrorMessage = "EmailAddressesFrom is Reqired.")]
        [EmailAddresses]
        public IEnumerable<EmailAddress> EmailAddressesFrom { get; set; }

        [Required(ErrorMessage = "Subject is Reqired.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Body is Reqired.")]
        public string Body { get; set; }
    }
}
