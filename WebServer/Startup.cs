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
using FrogsPond.Modules.FrogsContext.Domain.Settings;
using FrogsPond.Modules.FrogsContext.Domain.Services;
using FrogsPond.Modules.FrogsContext.Domain.Services.Implementations;
using FrogsPond.Modules.FrogsContext.Domain.RepositoryInterfaces;
using FrogsPond.Modules.FrogsContext.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            services.Configure<FrogDatabaseSettings>(
               Configuration.GetSection(nameof(FrogDatabaseSettings)));

            services.AddSingleton<IFrogDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FrogDatabaseSettings>>().Value);

            services.AddScoped<IFrogService, FrogService>();
            services.AddScoped<IFrogRepository, FrogMongoDBRepository>();

            AddAuthentication(services);
        }

        private void AddAuthentication(IServiceCollection services)
        {
            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // generated swagger json and swagger ui middleware
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "FrogsPond API"));

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
