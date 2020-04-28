using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Npgsql.Logging;
using System;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

    #if DEBUG
            //add sql statements and db logs to NLog as well
            NpgsqlLogManager.Provider = new NLogLoggingProvider();
            NpgsqlLogManager.IsParameterLoggingEnabled = true;
    #endif
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureLogging(logging =>
             {
                 logging.ClearProviders();
                 logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
             })
                .UseNLog()  // NLog: Setup NLog for Dependency injection
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    #region pass NpgsqlLoggingProvider to Nlog
    class NLogLoggingProvider : INpgsqlLoggingProvider
    {
        public NpgsqlLogger CreateLogger(string name)
        {
            return new NLogLogger(name);
        }
    }

    class NLogLogger : NpgsqlLogger
    {
        readonly Logger _log;

        internal NLogLogger(string name)
        {
            _log = LogManager.GetLogger(name);
        }

        public override bool IsEnabled(NpgsqlLogLevel level)
        {
            return _log.IsEnabled(ToNLogLogLevel(level));
        }

        public override void Log(NpgsqlLogLevel level, int connectorId, string msg, Exception exception = null)
        {
            var ev = new LogEventInfo(ToNLogLogLevel(level), "", msg);
            if (exception != null)
                ev.Exception = exception;
            if (connectorId != 0)
                ev.Properties["ConnectorId"] = connectorId;
            _log.Log(ev);
        }

        static NLog.LogLevel ToNLogLogLevel(NpgsqlLogLevel level)
        {
            switch (level)
            {
                case NpgsqlLogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case NpgsqlLogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case NpgsqlLogLevel.Info:
                    return NLog.LogLevel.Info;
                case NpgsqlLogLevel.Warn:
                    return NLog.LogLevel.Warn;
                case NpgsqlLogLevel.Error:
                    return NLog.LogLevel.Error;
                case NpgsqlLogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
  
    #endregion pass NpgsqlLoggingProvider to Nlog
}
