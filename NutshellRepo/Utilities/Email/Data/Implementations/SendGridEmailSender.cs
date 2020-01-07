using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Utilities.Email.Data.Implementations
{

    /// <summary>
    /// Implementation of IEmailSender that uses SendGrid.
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        public SendGridEmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        /// <summary>
        /// Sends an email via SendGrid API.
        /// </summary>
        /// <param name="details"></param>
        /// <returns>SendEmailResponse Object.</returns>
        public async Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details)
        {
            var apiKey = Configuration["NutshellRepoSendGridKey"];

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(
                    Configuration["SendEmailSettings:SendEmailFromEmail"],
                    Configuration["SendEmailSettings:SendEmailFromName"]
                );

            var subject = details.Subject;

            var to = new EmailAddress(details.ToEmail, details.ToName);

            var content = details.Content;

            var msg = MailHelper.CreateSingleEmail(
                                    from,
                                    to,
                                    subject,
                                    //content goes here if message type is text
                                    details.IsHTML ? null : content,
                                    //content goes here if message type is HTML
                                    details.IsHTML ? content : null
                                );

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return new SendEmailResponse();                
            }

            //if we have an error sending the email
            try
            {
                var bodyResult = await response.Body.ReadAsStringAsync();

                var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(bodyResult);

                var errorResponse = new SendEmailResponse
                {
                    Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
                };

                if (errorResponse.Errors == null || errorResponse.Errors.Count == 0)

                    errorResponse.Errors = new List<string>(new[] { "Unknown error from email sending service." });
                                
                return errorResponse;

            }
            catch (Exception)
            {
                return new SendEmailResponse
                {
                    Errors = new List<string>(new[] { "Unknown error occurred" })
                };
            }

        }
    }
}
