using System.Collections.Generic;

namespace NutshellRepo.Utilities.Email.Data.Implementations
{
    public class SendGridResponse
    {
        public List<SendGridResponseError> Errors { get; set; }
    }
}