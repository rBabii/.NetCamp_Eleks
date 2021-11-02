﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.AttachmentService.Models.Request
{
    public class SaveSingleImageRequest
    {
        public int Key { get; set; }
        public IFormFile Image { get; set; }
    }
}
