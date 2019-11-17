using Amazon.SQS.Model;

namespace PROJECT_NAME.Services.Processors
{
    public interface IMessageProcessor
    {
        bool CanProcess(string messageType);

        void Process(Message message);
    }
}