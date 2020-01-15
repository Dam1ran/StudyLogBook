using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NutshellRepo.Data.DB;
using NutshellRepo.Data.Security;
using NutshellRepo.Models;
using NutshellRepo.Utilities.Email.Data.Implementations;
using NutshellRepo.Utilities.Email.HTMLTemplates;

namespace NutshellRepo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<StudyLogBookDbContext>(option => 
                     option.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddIdentity<Member, IdentityRole>()
                    .AddEntityFrameworkStores<StudyLogBookDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {                
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/account/login";
                config.Cookie.Name = "Nutcracker.Cookie";
                config.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            });

            services.Configure<DataProtectionTokenProviderOptions>(options => 
                     options.TokenLifespan = TimeSpan.FromHours(1));

            services.AddSendGridEmailSender();

            services.AddTemplatedEmailSender();

            services.AddSingleton<DataProtectionPurposeStrings>();

            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                
                app.UseExceptionHandler("/Exception");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                //app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

           
        }
    }
}
