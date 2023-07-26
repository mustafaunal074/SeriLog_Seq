using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using SeriLog_Seq.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SeriLog_Seq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region appsettng.json Confg.
            //IConfigurationRoot configuration = new ConfigurationBuilder()  // Burada appsettings json dosyasýna eriþtik
            //       .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsetting.json")
            //       .Build();

            //Logger logger = new LoggerConfiguration() // Buradada yukarýda eriþtiðimz confg. dosyasýný serilog kütüphanesine kaynak olarak verdik
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

            //logger.Information("Selam, millet!");
            #endregion

            #region Runtime Confg.

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Seq("http://localhost:5341/")
                .MinimumLevel.Information() // minimum level belirlilmez ise info baz alýnýr
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning) //MinimumLevel information belirlenmiþtir lakin ‘Microsoft.AspNetCore’ namespace’i içerisindeki çalýþmalarda ‘MinimumLevel’ deðerini ‘Warning’ yapabiliriz.
                .Enrich.WithProperty("AppName", "Serilog Example") // atýlacak log'a dair ekstra prop aþaðýdaki gibidir
                .Enrich.WithProperty("Environment", "Development")
                .Enrich.WithProperty("Coder", "Mustafa")
                .Enrich.With(new ThreadIdEnricher())
                .CreateLogger();

            #endregion

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(config => config.ClearProviders())
            .UseSerilog();
    }
}
