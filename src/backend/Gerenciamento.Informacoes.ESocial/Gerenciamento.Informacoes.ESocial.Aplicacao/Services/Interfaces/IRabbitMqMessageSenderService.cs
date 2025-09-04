namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;

public interface IRabbitMqMessageSenderService
{
    Task SendMessageAsync(object message, string queueName);
}
