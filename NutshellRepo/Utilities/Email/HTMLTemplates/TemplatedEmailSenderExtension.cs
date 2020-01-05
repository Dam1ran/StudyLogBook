using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Utilities.Email.HTMLTemplates
{
    public static class TemplatedEmailSenderExtension
    {
        public static IServiceCollection AddTemplatedEmailSender(this IServiceCollection services)
        {            
            services.AddTransient<ITemplatedEmailSender, TemplatedEmailSender>();
                     
            return services;
        }
    }
}
