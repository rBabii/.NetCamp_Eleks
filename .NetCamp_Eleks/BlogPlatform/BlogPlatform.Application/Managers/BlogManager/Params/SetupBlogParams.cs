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
        public int UserId { get; set; } // Current Blog User

        [Required(ErrorMessage = "BlogUrl is Required.")]
        [RegularExpression("^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\\-]*[a-zA-Z0-9])\\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\\-]*[A-Za-z0-9])$", ErrorMessage = "Invalid Blog Url.")]
        public string BlogUrl { get; set; }

        [Required(ErrorMessage = "Title is Required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "SubTitle is Required.")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Visible is Required.")]
        public bool Visible { get; set; }

        [Required(ErrorMessage = "Preview Text is Required.")]
        [MinLength(100, ErrorMessage = "Preview Text must have at least 100 characters.")]
        [MaxLength(150, ErrorMessage = "Preview Text can include maximum 150 characters.")]
        public string PreviewText { get; set; }

        [Required(ErrorMessage = "Image is Required.")]
        public string BlogImage { get; set; }
    }
}
