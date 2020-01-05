using NutshellRepo.Utilities.Email.Data;
using NutshellRepo.Utilities.Email.Data.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NutshellRepo.Utilities.Email.HTMLTemplates
{
    public class TemplatedEmailSender : ITemplatedEmailSender
    {
        private readonly IEmailSender _EmailSender;

        public TemplatedEmailSender(IEmailSender aEmailSender)
        {
            _EmailSender = aEmailSender;
        }
        public async Task<SendEmailResponse> SendConfirmationEmailAsync(string email, string userName, string confirmationLink)
        {
            var templateText = default(string);
                        
            using (var reader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("NutshellRepo.Utilities.Email.HTMLTemplates.ConfirmEmail.html"), Encoding.UTF8))
            {                
                templateText = await reader.ReadToEndAsync();
            }

            // Replace special values with those inside the template
            templateText = templateText.Replace("--UserName--", userName)
                                       .Replace("--ConfirmationButtonLink--", confirmationLink)
                                       .Replace("--WebSiteLink--", "https://localhost:44354/");

            return await _EmailSender.SendEmailAsync(
                        new SendEmailDetails() 
                        {
                            ToName = userName,
                            ToEmail = email,
                            Subject = "Your Email Confirmation Link",
                            IsHTML = true,
                            Content = templateText
                        });
            
        }
    }
}
