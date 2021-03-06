using System;
using System.IO;
using AutoMapper;
using Api.Configurations.Jwt;
using Api.Middlewares;
using Business;
using Business.Customer;
using Business.Dashboard;
using Business.Feedback;
using Business.Import;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Security;
using Service.Caching;
using Business.Parameter;
using Business.Transaction;
using Business.User;
using Dal.Repositories.File;
using Google.Cloud.Diagnostics.AspNetCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Service.Email;
using Service.File;
using Service.Slack;

namespace Api
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
            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true; //prevent automatic 400 response
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });

            //Injecting the db context
            services.AddDbContext<Dal.Db.BlackCoveredLedgerDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostgresDbContext")));
            services.AddTransient<Dal.IUnitOfWork, Dal.UnitOfWork>();

            //Injecting the identity manager
            services.AddIdentity<Dal.Entities.Identity.ApplicationUser,
                Dal.Entities.Identity.ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<Dal.Db.BlackCoveredLedgerDbContext>()
                .AddDefaultTokenProviders();

            // Add functionality to inject IOptions<T>
            services.AddOptions();
            // Add our Config object so it can be injected
            services.Configure<AppSettings>(Configuration.GetSection("Keys"));

            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<ICustomerBusiness, CustomerBusiness>();
            services.AddTransient<ITransactionBusiness, TransactionBusiness>();
            services.AddTransient<IParameterBusiness, ParameterBusiness>();
            services.AddTransient<IFeedbackBusiness, FeedbackBusiness>();
            services.AddTransient<IDashboardBusiness, DashboardBusiness>();
            services.AddTransient<IImportBusiness, ImportBusiness>();
            services.AddTransient<ISecurity, JwtSecurity>();

            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISlackService, SlackService>();
            services.AddTransient<IFileService, FileDiskStorageService>();
            services.AddTransient<IFileRepository, FileRepository>();

            services.AddAutoMapper(typeof(MappingProfile));

            //todo: configure cors properly
            services.AddCors(config =>
            {
                var policy = new CorsPolicy();
                policy.Headers.Add("*");
                policy.Methods.Add("*");
                policy.Origins.Add("*");
                //policy.SupportsCredentials = true;
                config.AddPolicy("policy", policy);
            });

            services.ConfigureJwtAuthentication(Configuration);
            services.ConfigureJwtAuthorization();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration["Logging:UseGoogle"] != "false")
            {
                loggerFactory.AddGoogle(app.ApplicationServices, "karakapli");
            }

            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //request handling
            app.UseRequestMiddleware();

            app.UseRouting();
            app.UseCors("policy");

            const string cacheMaxAge = "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append(
                        "Cache-Control", $"public, max-age={cacheMaxAge}");
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var logger = app.ApplicationServices.GetService<ILogger<Startup>>();

            logger.LogInformation($"application started working on {Environment.MachineName} at {DateTime.UtcNow:u}");
        }
    }
}
