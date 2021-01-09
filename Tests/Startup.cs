using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using AutoMapper;
using Business;
using Business.Customer;
using Business.Import;
using Service.Caching;
using Common;
using Business.Transaction;
using Business.Parameter;
using Microsoft.EntityFrameworkCore.Storage;

[assembly: TestFramework("Tests.Startup", "Tests")]
namespace Tests
{
    public class Startup : DependencyInjectionTestFramework
    {
        public static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new InMemoryDatabaseRoot();

        public Startup(IMessageSink messageSink) : base(messageSink)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            var configuration = GetApplicationConfiguration();

            services.AddDbContext<Dal.Db.BlackCoveredLedgerDbContext>(
                opt =>
                    opt.UseInMemoryDatabase("UnitTestDb", InMemoryDatabaseRoot)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddTransient<Dal.IUnitOfWork, Dal.UnitOfWork>();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<AppSettings>(configuration.GetSection("Keys"));

            services.AddTransient<ICustomerBusiness, CustomerBusiness>();
            services.AddTransient<ITransactionBusiness, TransactionBusiness>();
            services.AddTransient<IParameterBusiness, ParameterBusiness>();
            services.AddTransient<IImportBusiness, ImportBusiness>();

            services.AddTransient<ICacheService, CacheService>();

            services.AddAutoMapper(typeof(MappingProfile));
        }

        private IConfiguration GetApplicationConfiguration()
        {
            return new ConfigurationBuilder()
               .AddJsonFile(GetApplicationPath("settings.json"))
               .Build();
        }

        private string GetApplicationPath(string fileName)
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return Path.Combine(appRoot, fileName);
        }
    }
}
