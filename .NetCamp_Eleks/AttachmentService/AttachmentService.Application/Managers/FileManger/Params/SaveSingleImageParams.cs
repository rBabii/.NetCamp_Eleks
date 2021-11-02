using AttachmentService.Application.Managers.FileManger.Params.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentService.Application.Managers.FileManger.Params
{
    public class SaveSingleImageParams
    {
        [Required(ErrorMessage = "Key field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Key field cant be less then 1")]
        public int Key { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; }
    }
}
