using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PROJECT_NAME
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //#if (AddSqsPublisher)
                .UseUrls("http://*:5100", "https://*:5101")
                //#endif
                //#if (AddSqsConsumer)
                .UseUrls("http://*:5200", "https://*:5201")
                //#endif
                .Build();

            webHost.Run();
        }
    }
}
