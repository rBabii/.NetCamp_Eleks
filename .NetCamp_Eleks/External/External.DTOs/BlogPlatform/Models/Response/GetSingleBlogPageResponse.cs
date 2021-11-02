﻿using External.DTOs.BlogPlatform.Models.Response.Childs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Response
{
    public class GetSingleBlogPageResponse
    {
        public List<GetSingleBlogPagePostItem> Posts { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
