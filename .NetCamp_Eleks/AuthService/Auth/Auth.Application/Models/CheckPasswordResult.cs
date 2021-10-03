using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Models
{
    public class CheckPasswordResult
    {
        public bool IsVerified { get; set; }
        public bool NeedsUpgrade { get; set; }
    }
}
