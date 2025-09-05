using RabbitMQ.Client;

namespace Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Messaging;

public interface IRabbitMqConnection : IAsyncDisposable
{
    Task<IConnection> GetConnectionAsync();
}
