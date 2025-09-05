using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Messaging;
using RabbitMQ.Client;

namespace Gerenciamento.Informacoes.ESocial.Infra.Messaging.Connections;

public class RabbitMqConnection : IRabbitMqConnection
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;

    public RabbitMqConnection(string hostName, string userName, string password)
    {
        _factory = new ConnectionFactory
        {
            HostName = hostName,
            UserName = userName,
            Password = password
        };
    }

    public async Task<IConnection> GetConnectionAsync()
    {
        if (_connection is { IsOpen: true })
            return _connection;

        _connection = await _factory.CreateConnectionAsync();
        return _connection;
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
        {
            await Task.Run(() => _connection.Dispose());
        }
    }
}
