using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Azure.Security.KeyVault.Secrets;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Options.v1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public class CustomerUpdateSenderServiceBus : ICustomerUpdateSender
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ILogger<CustomerUpdateSenderServiceBus> _logger;

        public CustomerUpdateSenderServiceBus(IOptions<AzureServiceBusConfiguration> serviceBusOptions, ILogger<CustomerUpdateSenderServiceBus> logger)
        {
            _connectionString = serviceBusOptions.Value.ConnectionString;
            _queueName = serviceBusOptions.Value.QueueName;
            _logger = logger;
        }

        public async void SendCustomer(Customer customer)
        {
            var secret = new SecretClient(new Uri("https://ozzkeyvault.vault.azure.net/"), new DefaultAzureCredential());
            var connectionString = secret.GetSecret("AzBusSecret").Value;

            // todo add exception handling when queue is not accessible
            await using (var client = new ServiceBusClient(connectionString.Value))
            {
                var sender = client.CreateSender(_queueName);

                var json = JsonConvert.SerializeObject(customer);
                var message = new ServiceBusMessage(json);

                await sender.SendMessageAsync(message);
            }
        }
    }
}
