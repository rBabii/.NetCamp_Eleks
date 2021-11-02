using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using BlogPlatform.Domain.AgregatesModel.UserAgregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.UserManager.Resut.Childs
{
    public class User
    {
        public int Id { get; set; }
        public int AuthResourceUserId { get; set; }  // from Auth Claims
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string Email { get; set; } // from Auth Claims
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
