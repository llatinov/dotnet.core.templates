using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using PROJECT_NAME.Sqs.Models;

namespace PROJECT_NAME.Services.Processors
{
    public class MovieMessageProcessor : IMessageProcessor
    {
        private readonly ILogger<MovieMessageProcessor> _logger;

        public MovieMessageProcessor(ILogger<MovieMessageProcessor> logger)
        {
            _logger = logger;
        }

        public bool CanProcess(string messageType)
        {
            return messageType == typeof(Movie).Name;
        }

        public void Process(Message message)
        {
            _logger.LogInformation($"MovieMessageProcessor invoked with: {message.Body}");
        }
    }
}