using NutshellRepo.Utilities.Email.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Utilities.Email.Data
{
    public interface IEmailSender
    {
        Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}
