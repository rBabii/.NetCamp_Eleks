using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Params
{
    public class SearchBlogsParams
    {
        [RegularExpression("^$|(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\\-]*[a-zA-Z0-9])\\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\\-]*[A-Za-z0-9])$", ErrorMessage = "Invalid Blog Url.")]
        public string BlogUrl { get; set; }

        [Required(ErrorMessage = "Page Number is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Page Number cant be less then 1")]
        public int PageNumber { get; set; }

        [Required(ErrorMessage = "Page Size is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Page Size cant be less then 1")]
        public int PageSize { get; set; }

        [MaxLength(150, ErrorMessage = "Search Text can include maximum 150 characters.")]
        public string SearchText { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Blog Id cant be less then 0")]
        public int BlogId { get; set; }

        public bool LoadRelatedEntities { get; set; } = false;
    }
}
