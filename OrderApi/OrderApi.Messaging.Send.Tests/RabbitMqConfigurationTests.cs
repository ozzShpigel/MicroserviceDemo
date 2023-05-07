using OrderApi.Messaging.Receive.Options.v1;
using Xunit;

namespace OrderApi.Tests.Messaging.Receive.Options.v1
{
    public class RabbitMqConfigurationTests
    {
        [Fact]
        public void Properties_Should_Set_Correctly()
        {
            // Arrange
            var config = new RabbitMqConfiguration
            {
                Hostname = "testhostname",
                QueueName = "testqueuename",
                UserName = "testusername",
                Password = "testpassword",
                Enabled = true
            };

            // Act

            // Assert
            Assert.Equal("testhostname", config.Hostname);
            Assert.Equal("testqueuename", config.QueueName);
            Assert.Equal("testusername", config.UserName);
            Assert.Equal("testpassword", config.Password);
            Assert.True(config.Enabled);
        }
    }
}
