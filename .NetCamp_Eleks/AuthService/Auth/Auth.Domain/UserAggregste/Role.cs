using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.UserAggregste
{
    public class Role
    {
        public Enums.Role Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Permisions { get; set; }
    }
}
