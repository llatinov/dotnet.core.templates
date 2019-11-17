﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace PROJECT_NAME.Sqs
{
    public interface ISqsClient
    {
        string GetQueueName();

        Task CreateQueue();

        Task<SqsStatus> GetQueueStatus();

        Task<List<Message>> GetMessagesAsync(CancellationToken cancellationToken = default);

        Task PostMessageAsync(string messageBody, string messageType);

        Task DeleteMessageAsync(string receiptHandle);

        Task RestoreFromDeadLetterQueue(CancellationToken cancellationToken = default);
    }
}