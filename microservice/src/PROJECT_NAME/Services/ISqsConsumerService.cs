using System.Threading.Tasks;
using PROJECT_NAME.Sqs;

namespace PROJECT_NAME.Services
{
    public interface ISqsConsumerService
    {
        Task<SqsStatus> GetStatus();

        void StartConsuming();

        void StopConsuming();

        Task ReprocessMessages();
    }
}