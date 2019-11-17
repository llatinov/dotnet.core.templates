//#if (AddSqsPublisher || AddSqsConsumer)
using Amazon.SQS;
//#endif
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//#if (AddHealthChecks)
using PROJECT_NAME.HealthChecks;
//#endif
using PROJECT_NAME.Middleware;
//#if (AddSqsConsumer)
using PROJECT_NAME.Services;
//#endif
//#if (AddSqsPublisher || AddSqsConsumer)
using PROJECT_NAME.Sqs;
//#endif

namespace PROJECT_NAME
{
    public class Startup
    {
        //#if (AddSqsPublisher || AddSqsConsumer)
        private readonly AppConfig _appConfig = new AppConfig();
        //#endif
        public Startup()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
            //#if (AddSqsPublisher || AddSqsConsumer)
            Configuration.Bind(_appConfig);
            _appConfig.AwsSettings.UpdateFromEnvironment();
            //#endif
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<AppConfig>(Configuration);
            //#if (AddSqsPublisher || AddSqsConsumer)
            services.AddSingleton<IAmazonSQS>(x => SqsClientFactory.CreateClient(_appConfig.AwsSettings));
            services.AddSingleton<ISqsClient, SqsClient>();
            //#endif
            ////#if (AddHealthChecks)
            services.AddHealthChecks()
                //#if (AddSqsPublisher || AddSqsConsumer)
                .AddCheck<SqsHealthCheck>("SQS Health Check")
                //#endif
                .AddCheck<VersionHealthCheck>("Version Health Check");
            //#endif
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env
            //#if (AddSqsPublisher || AddSqsConsumer)
            , ISqsClient sqsClient
            //#endif
            //#if (AddSqsConsumer)
            , ISqsConsumerService sqsConsumerService
            //#endif
            )
        {
            app.UseMiddleware<HttpExceptionMiddleware>();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //#if (AddHealthChecks)
                endpoints.MapHealthChecks();
                //#endif
            });
            //#if (AddSqsPublisher || AddSqsConsumer)
            sqsClient.CreateQueue().Wait();
            //#endif
            //#if (AddSqsConsumer)
            sqsConsumerService.StartConsuming();
            //#endif
        }
    }
}
