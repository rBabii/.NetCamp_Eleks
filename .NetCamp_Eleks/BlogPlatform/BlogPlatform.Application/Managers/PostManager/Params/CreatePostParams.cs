using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.PostManager.Params
{
    public class CreatePostParams
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        [Required]
        public bool Visible { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public string HeaderContent { get; set; }
        [Required]
        public string MainContent { get; set; }
        [Required]
        public string FooterContent { get; set; }
    }
}
