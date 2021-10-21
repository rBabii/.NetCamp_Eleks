using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Params
{
    public class SetupBlogParams
    {
        [Required(ErrorMessage = "UserId is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId cant be less then 1")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "BlogUrl is Required.")]
        public string BlogUrl { get; set; }

        [Required(ErrorMessage = "Title is Required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "SubTitle is Required.")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Visible is Required.")]
        public bool Visible { get; set; }
    }
}
