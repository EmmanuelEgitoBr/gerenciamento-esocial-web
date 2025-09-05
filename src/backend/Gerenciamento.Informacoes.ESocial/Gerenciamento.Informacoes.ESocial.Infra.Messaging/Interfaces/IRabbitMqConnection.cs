using RabbitMQ.Client;

namespace Gerenciamento.Informacoes.ESocial.Infra.Messaging.Interfaces;

public interface IRabbitMqConnection : IAsyncDisposable
{
    Task<IConnection> GetConnectionAsync();
}
