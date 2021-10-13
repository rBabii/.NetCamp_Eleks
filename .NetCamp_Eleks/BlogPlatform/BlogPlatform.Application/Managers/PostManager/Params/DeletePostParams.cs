using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.PostManager.Params
{
    public class DeletePostParams
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
