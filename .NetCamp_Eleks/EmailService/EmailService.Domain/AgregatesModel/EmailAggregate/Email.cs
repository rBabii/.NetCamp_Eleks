using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Domain.AgregatesModel.EmailAggregate
{
    public class Email
    {
        public IEnumerable<EmailAddress> EmailAddressesTo { get; set; }
        public IEnumerable<EmailAddress> EmailAddressesFrom { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
