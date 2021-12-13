using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentService.Application.Managers.FileManger.Result
{
    public class SaveSingleImageResult : BaseResult
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public int Key { get; set; }
        public SaveSingleImageResult(string fileName, int key, string url, Error error = null)
            : base(error)
        {
            FileName = fileName;
            Key = key;
            Url = url;
        }
    }
}
