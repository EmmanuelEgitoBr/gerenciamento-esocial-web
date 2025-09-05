namespace Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Messaging;

public interface IRabbitMqMessageSenderService
{
    Task SendMessageAsync(object message, string queueName);
}
