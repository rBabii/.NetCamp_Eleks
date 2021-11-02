using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentService.Application.Managers.FileManger.Result
{
    public class SaveUserImageResult : BaseResult
    {
        public SaveUserImageResult(Error error = null)
            : base(error)
        {

        }
    }
}
