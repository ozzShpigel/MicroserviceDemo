using CustomerApi.Messaging.Send.Options.v1;
using Moq;
using RabbitMQ.Client;
using System;

namespace CustomerApi.Messaging.Send.Tests
{
    public class RabbitMqConfigurationTests
    {

        [Fact]
        public void RabbitMqConfigurationProperties()
        {
            // Arrange
            var rabbitMqConfiguration = new RabbitMqConfiguration();
            rabbitMqConfiguration.UserName = "oz";
            rabbitMqConfiguration.Hostname = "rabbitmq";
            rabbitMqConfiguration.QueueName = "CustomerQueue";
            rabbitMqConfiguration.Password = "password";

            // Act
            var userName = rabbitMqConfiguration.UserName;
            var password = rabbitMqConfiguration.Password;
            var hostname = rabbitMqConfiguration.Hostname;
            var queueName = rabbitMqConfiguration.QueueName;

            Assert.True(userName.Equals("oz"));
            Assert.True(password.Equals("password"));
            Assert.True(hostname.Equals("rabbitmq"));
            Assert.True(queueName.Equals("CustomerQueue"));
        }
    }
}