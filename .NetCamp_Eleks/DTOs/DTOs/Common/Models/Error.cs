using DTOs.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Common.Models
{
    public class Error
    {
        public IEnumerable<string> ErrorMessages { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}
