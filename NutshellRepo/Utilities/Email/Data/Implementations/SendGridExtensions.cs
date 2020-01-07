using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Utilities.Email.Data.Implementations
{
    public static class SendGridExtensions
    {   
         /// <summary>
         /// Adds Transient Scope of IEmailSender service.
         /// </summary>
         /// <param name="services"></param>
         /// <returns>Service Collection With added IEmailSender.</returns>
        public static IServiceCollection AddSendGridEmailSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            return services;
        }

    }    

}
