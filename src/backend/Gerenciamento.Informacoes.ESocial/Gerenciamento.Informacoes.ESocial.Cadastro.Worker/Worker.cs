using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Clients;
using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Messaging;
using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.ProcessoLogs;
using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Settings;
using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker;

public class Worker(ILogger<Worker> logger,
    IEmailApiClient emailApiClient,
    IProcessosApiClient processosApiClient,
    IRabbitMqConnection connection,
    RabbitMqSettings rabbitMqSettings) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IEmailApiClient _emailApiClient = emailApiClient;
    private readonly IProcessosApiClient _processosApiClient = processosApiClient;
    private readonly IRabbitMqConnection _connection = connection;
    private IConnection? _rabbitConnection;
    private IChannel? _channel;
    private readonly string _queueName = rabbitMqSettings.QueueName;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _rabbitConnection = await _connection.GetConnectionAsync();
        var channel = await _rabbitConnection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (ch, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<MessageModel>(messageJson);

                if(message  == null)
                {
                    _logger.LogWarning("Mensagem recebida foi nula!");
                    return;
                }

                bool emailEnviado = false;
                string? erroEnvio = null;

                var response = await _emailApiClient.SendEmailAsync(message.Email!);
                emailEnviado = response.Success;

                if (!emailEnviado) erroEnvio = response.ErrorMessage;

                var logStatus = new LogStatusCadastro
                {
                    TrabalhadorId = message.TrabalhadorId,
                    EmailTrabalhador = message.EmailTrabalhador,
                    IsEmailEnviado = emailEnviado,
                    StatusCadastro = message.StatusCadastro,
                    Pendencias = message.Pendencias
                };

                await _processosApiClient.RegistrarLogStatusAsync(logStatus);

                if (!emailEnviado)
                {
                    var logEnvioEmail = new LogEnvioEmail
                    {
                        TrabalhadorId = message.TrabalhadorId,
                        EmailTrabalhador = message.EmailTrabalhador,
                        DescricaoLog = erroEnvio 
                    };

                    await _processosApiClient.RegistrarLogEnvioEmailAsync(logEnvioEmail);
                }

                await _channel!.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consumir mensagem via RabbitMQ");
            }
        };

        await _channel!.BasicConsumeAsync(queue: _queueName, autoAck: false, consumer: consumer);
    }
}
