using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Services.Email.Models
{
    public class SendEmailModel
    {
        public List<EmailAddress> EmailAddressesTo { get; set; }
        public List<EmailAddress> EmailAddressesFrom { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
