using NutshellRepo.Utilities.Email.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Utilities.Email.HTMLTemplates
{
    public interface ITemplatedEmailSender
    {
        Task<SendEmailResponse> SendConfirmationEmailAsync(string email, string userName, string confirmationLink);
    }
}
