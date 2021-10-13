﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.BlogAgregate
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> Get();
        Blog Get(int Id);
        Blog GetByUserId(int userId);
        Blog GetByUrl(string url);
        bool Delete(Blog blog);
        Blog AddOrUpdate(Blog blog);
    }
}
