using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.UserAggregate
{
    public class Role
    {
        public int Id { get; set; }
        public Enums.Role RoleEnum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
