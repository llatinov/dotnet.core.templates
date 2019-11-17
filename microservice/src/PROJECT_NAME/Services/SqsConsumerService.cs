﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using PROJECT_NAME.Services.Processors;
using PROJECT_NAME.Sqs;

namespace PROJECT_NAME.Services
{
    public class SqsConsumerService : ISqsConsumerService
    {
        private readonly ISqsClient _sqsClient;
        private readonly IEnumerable<IMessageProcessor> _messageProcessors;
        private readonly ILogger<SqsConsumerService> _logger;

        private CancellationTokenSource _tokenSource;

        public SqsConsumerService(ISqsClient sqsClient, IEnumerable<IMessageProcessor> messageProcessors, ILogger<SqsConsumerService> logger)
        {
            _sqsClient = sqsClient;
            _messageProcessors = messageProcessors;
            _logger = logger;
        }

        public async Task<SqsStatus> GetStatus()
        {
            var status = await _sqsClient.GetQueueStatus();
            status.IsConsuming = IsConsuming();

            return status;
        }

        public void StartConsuming()
        {
            if (IsConsuming())
                return;

            _tokenSource = new CancellationTokenSource();
            ProcessAsync();
        }

        public void StopConsuming()
        {
            if (!IsConsuming())
                return;

            _tokenSource.Cancel();
        }

        private bool IsConsuming()
        {
            return _tokenSource != null && !_tokenSource.Token.IsCancellationRequested;
        }

        private async void ProcessAsync()
        {
            try
            {
                while (!_tokenSource.Token.IsCancellationRequested)
                {
                    var messages = await _sqsClient.GetMessagesAsync(_tokenSource.Token);
                    messages.ForEach(ProcessMessage);
                }
            }
            catch (OperationCanceledException)
            {
                //operation has been canceled but it shouldn't be propagated
            }
        }

        private async void ProcessMessage(Message message)
        {
            try
            {
                var messageType = message.MessageAttributes.SingleOrDefault(x => x.Key == MessageAttributes.MessageType).Value;
                if (messageType == null)
                {
                    throw new Exception($"No '{MessageAttributes.MessageType}' attribute present in message");
                }

                var processor = _messageProcessors.SingleOrDefault(x => x.CanProcess(messageType.StringValue));
                if (processor == null)
                {
                    throw new Exception($"No processor found for message type '{messageType.StringValue}'");
                }

                processor.Process(message);
                await _sqsClient.DeleteMessageAsync(message.ReceiptHandle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot process message [id: {message.MessageId}, receiptHandle: {message.ReceiptHandle}, body: {message.Body}] from queue {_sqsClient.GetQueueName()}");
            }
        }

        public async Task ReprocessMessages()
        {
            await _sqsClient.RestoreFromDeadLetterQueue();
        }
    }
}