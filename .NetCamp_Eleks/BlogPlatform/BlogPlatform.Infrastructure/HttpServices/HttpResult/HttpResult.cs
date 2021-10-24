using External.DTOs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.HttpServices.HttpResult
{
    public class HttpResult<T> where T : class
    {
        public T ResponseObject { get; set; }
        public Error Error { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool IsSuccess 
        {
            get
            {
                return (int)HttpStatusCode >= 200 && (int)HttpStatusCode < 400;
            }
        }
        public bool IsError
        {
            get
            {
                return (int)HttpStatusCode >= 500;
            }
        }

        public HttpResult(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
