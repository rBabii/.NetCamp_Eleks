using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.PostManager.Params
{
    public class GetSinglePostParams
    {
        [Required(ErrorMessage = "PostId is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "PostId cant be less then 1")]
        public int PostId { get; set; }
    }
}
