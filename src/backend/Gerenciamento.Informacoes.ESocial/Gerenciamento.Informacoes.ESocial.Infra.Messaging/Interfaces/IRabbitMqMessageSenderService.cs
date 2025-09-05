namespace Gerenciamento.Informacoes.ESocial.Infra.Messaging.Interfaces;

public interface IRabbitMqMessageSenderService
{
    Task SendMessageAsync(object message, string queueName);
}
