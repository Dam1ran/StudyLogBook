using NutshellRepo.Utilities.Email.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Utilities.Email.Data
{
    public interface IEmailSender
    {        
        /// <summary>
        /// Email Sender Interface Method.
        /// </summary>
        /// <param name="details"></param>
        /// <returns>SendEmailResponse Object.</returns>
        Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}
