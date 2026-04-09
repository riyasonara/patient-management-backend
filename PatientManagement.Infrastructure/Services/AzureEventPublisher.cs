using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace PatientManagement.Infrastructure.Services;

public class AzureEventPublisher
{
    private readonly ServiceBusClient _client;
    private readonly string _queueName = "patient-created-queue";

    public AzureEventPublisher(IConfiguration config)
    {
        _client = new ServiceBusClient(config["ServiceBus:ConnectionString"]);
    }

    public async Task PublishAsync(object payload)
    {
        var sender = _client.CreateSender(_queueName);

        var message = new ServiceBusMessage(JsonSerializer.Serialize(payload));

        await sender.SendMessageAsync(message);
    }
}
