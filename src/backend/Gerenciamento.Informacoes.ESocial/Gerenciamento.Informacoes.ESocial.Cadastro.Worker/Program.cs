using Gerenciamento.Informacoes.ESocial.Cadastro.Worker;
using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Clients;
using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Connections;
using Refit;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<RabbitMqConnection>(sp =>
            new RabbitMqConnection("localhost", "guest", "guest"));

        services.AddRefitClient<IEmailApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7013"));

        services.AddRefitClient<IProcessosApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7013"));

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
