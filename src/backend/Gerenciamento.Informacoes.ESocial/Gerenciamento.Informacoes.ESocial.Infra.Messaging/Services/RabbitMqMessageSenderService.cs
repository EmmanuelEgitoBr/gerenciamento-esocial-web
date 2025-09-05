using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Gerenciamento.Informacoes.ESocial.Infra.Messaging.Services;

public class RabbitMqMessageSenderService : IRabbitMqMessageSenderService
{
    private readonly IRabbitMqConnection _connection;

    public RabbitMqMessageSenderService(IRabbitMqConnection connection)
    {
        _connection = connection;
    }

    public async Task SendMessageAsync(object message, string queueName)
    {
        await using var connection = await _connection.GetConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            mandatory: false,
            body: body.AsMemory()
        );
    }
}
