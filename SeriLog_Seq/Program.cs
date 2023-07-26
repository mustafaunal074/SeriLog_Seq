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
            //IConfigurationRoot configuration = new ConfigurationBuilder()  // Burada appsettings json dosyas�na eri�tik
            //       .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsetting.json")
            //       .Build();

            //Logger logger = new LoggerConfiguration() // Buradada yukar�da eri�ti�imz confg. dosyas�n� serilog k�t�phanesine kaynak olarak verdik
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
                .MinimumLevel.Information() // minimum level belirlilmez ise info baz al�n�r
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning) //MinimumLevel information belirlenmi�tir lakin �Microsoft.AspNetCore� namespace�i i�erisindeki �al��malarda �MinimumLevel� de�erini �Warning� yapabiliriz.
                .Enrich.WithProperty("AppName", "Serilog Example") // at�lacak log'a dair ekstra prop a�a��daki gibidir
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
