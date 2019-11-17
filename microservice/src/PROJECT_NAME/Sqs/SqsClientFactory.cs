using Amazon.SQS;

namespace PROJECT_NAME.Sqs
{
    public class SqsClientFactory
    {
        public static AmazonSQSClient CreateClient(AppConfig.AwsConfig awsConfig)
        {
            var sqsConfig = new AmazonSQSConfig { ServiceURL = awsConfig.ServiceUrl };
            var awsCredentials = new AwsCredentials(awsConfig);
            return new AmazonSQSClient(awsCredentials, sqsConfig);
        }
    }
}