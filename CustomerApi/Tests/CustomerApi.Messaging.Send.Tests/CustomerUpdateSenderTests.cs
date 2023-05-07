using System;
using System.Collections.Generic;
using System.Text;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Options.v1;
using CustomerApi.Messaging.Send.Sender.v1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace CustomerApi.Tests.Messaging.Send.Sender.v1
{
    public class CustomerUpdateSenderTests
    {
        private readonly Mock<ILogger<CustomerUpdateSender>> _loggerMock;
        private readonly Mock<IOptions<RabbitMqConfiguration>> _rabbitMqOptionsMock;

        public CustomerUpdateSenderTests()
        {
            _loggerMock = new Mock<ILogger<CustomerUpdateSender>>();
            _rabbitMqOptionsMock = new Mock<IOptions<RabbitMqConfiguration>>();
        }

        [Fact]
        public void SendCustomer_ConnectionNotCreated_LogsError()
        {
            // Arrange
            _rabbitMqOptionsMock.SetupGet(x => x.Value).Returns(new RabbitMqConfiguration
            {
                QueueName = "test-queue",
                Hostname = "test-hostname",
                UserName = "test-username",
                Password = "test-password"
            });

            var sender = new CustomerUpdateSender(_rabbitMqOptionsMock.Object, _loggerMock.Object);

            // Act
            sender.SendCustomer(new Customer());

            // Assert
            _loggerMock.Verify(
                x => x.LogError(It.IsAny<Exception>(), "Could not create connection: {0}"),
                Times.Once);
        }

        [Fact]
        public void SendCustomer_ConnectionCreated_SendsMessage()
        {
            // Arrange
            var mockConnection = new Mock<IConnection>();
            _rabbitMqOptionsMock.SetupGet(x => x.Value).Returns(new RabbitMqConfiguration
            {
                QueueName = "test-queue",
                Hostname = "test-hostname",
                UserName = "test-username",
                Password = "test-password"
            });

            var sender = new CustomerUpdateSender(_rabbitMqOptionsMock.Object, _loggerMock.Object);

            sender.GetType().GetField("_connection", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(sender, mockConnection.Object);

            // Act
            sender.SendCustomer(new Customer());

            // Assert
            mockConnection.Verify(x => x.CreateModel(), Times.Once);
        }
    }
}
