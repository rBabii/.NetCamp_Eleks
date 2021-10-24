using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.Options.EmailService
{
    public class EmailServiceOptions
    {
        public int PortNumber { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public bool UseAuth { get; set; }
        public bool UseSsl { get; set; }
    }
}
