using CustomerApi.Messaging.Send.Options.v1;

namespace CustomerApi.Messaging.Send.Tests
{
    public class AzureServiceBusConfigurationTests
    {
        [Fact]
        public void Constructor_Sets_Properties_Correctly()
        {
            // Arrange
            var expectedConnectionString = "Endpoint=sb://your-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=your-key";
            var expectedQueueName = "your-queue-name";

            // Act
            var configuration = new AzureServiceBusConfiguration
            {
                ConnectionString = expectedConnectionString,
                QueueName = expectedQueueName
            };

            // Assert
            Assert.Equal(expectedConnectionString, configuration.ConnectionString);
            Assert.Equal(expectedQueueName, configuration.QueueName);
        }
    }
}