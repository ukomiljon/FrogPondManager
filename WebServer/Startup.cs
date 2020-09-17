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
using FrogsPond.Modules.AccountsContext.Data;
using FrogsPond.Modules.AccountsContext.Controllers;

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
            //services.AddDbContext<DataContext>();
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen();

            services.AddControllers()
           .AddControllersAsServices();

            // configure strongly typed settings object
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services  
            //services.AddScoped<AccountsController>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IAccountRepository, MongoDbRepository>();

            //services.AddSingleton<EmailService>(provider =>
            //{
            //    return new EmailService(null);
            //});

            //services.AddSingleton<AccountService>(provider =>
            //{                 
            //    return new AccountService(null, null, null);
            //});

            //services.AddSingleton<AccountsController>(provider =>
            //{ 
            //    return new AccountsController(null, null);
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
