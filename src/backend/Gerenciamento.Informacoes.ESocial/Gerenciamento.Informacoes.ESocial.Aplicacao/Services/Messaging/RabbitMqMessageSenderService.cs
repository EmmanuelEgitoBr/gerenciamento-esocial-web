using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Messaging;

public class RabbitMqMessageSenderService : IRabbitMqMessageSenderService
{
    private readonly string _hostName;
    private readonly string _username;
    private readonly string _password;

    public RabbitMqMessageSenderService()
    {
        _hostName = "localhost";
        _username = "guest";
        _password = "guest";
    }

    public async Task SendMessageAsync(object message, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            UserName = _username,
            Password = _password
        };

        await using var connection = await factory.CreateConnectionAsync();
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
