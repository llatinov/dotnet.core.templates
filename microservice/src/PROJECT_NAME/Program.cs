using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
//#if (AddSerilog)
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
//#endif

namespace PROJECT_NAME
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //#if (AddSerilog)
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(new CompactJsonFormatter())
                .CreateLogger();

            //#endif
            var webHost = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //#if (AddSerilog)
                .UseSerilog()
                //#endif
                //#if (AddSqsPublisher)
                .UseUrls("http://*:5100")
                //#endif
                //#if (AddSqsConsumer)
                .UseUrls("http://*:5200")
                //#endif
                .Build();

            webHost.Run();
        }
    }
}
