using External.Result.Base;

namespace EmailService.Application.Managers.EmailManager.Result
{
    public class SendEmailResult : BaseResult
    {
        public SendEmailResult(Error error = null)
            : base(error)
        {

        }
    }
}
