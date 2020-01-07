using System.Collections.Generic;

namespace NutshellRepo.Utilities.Email.Data.Implementations
{
    /// <summary>
    /// Contains List of Errors in SendGrid response format.
    /// </summary>     
    public class SendGridResponse
    {
        public List<SendGridResponseError> Errors { get; set; }

    }
}