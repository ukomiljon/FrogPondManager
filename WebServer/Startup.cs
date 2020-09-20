using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;
using FrogsPond.Modules.AccountsContext.Domain.Services;
using AutoMapper;
using FrogsPond.Modules.AccountsContext.Domain;
using FrogsPond.Modules.AccountsContext.Controllers;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using FrogsPond.Modules.AccountsContext.Data;

namespace WebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {             
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen();

            services.AddControllers();
            
            services.Configure<SmtpSettings>(
               Configuration.GetSection(nameof(SmtpSettings)));

            services.AddSingleton<ISmtpSettings>(sp =>
                sp.GetRequiredService<IOptions<SmtpSettings>>().Value);

            services.Configure<AccountDatabaseSettings>(
               Configuration.GetSection(nameof(AccountDatabaseSettings)));

            services.AddSingleton<IAccountDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AccountDatabaseSettings>>().Value);

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccountRepository, AccountMongoDBRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // generated swagger json and swagger ui middleware
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/api/v1/swagger.json", "FrogsPond API"));

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            // global error handler
            //app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            //app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
