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
                .Build();

            webHost.Run();
        }
    }
}
