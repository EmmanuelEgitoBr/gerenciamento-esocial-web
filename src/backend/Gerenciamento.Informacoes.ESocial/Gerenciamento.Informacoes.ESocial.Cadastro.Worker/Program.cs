using Gerenciamento.Informacoes.ESocial.Cadastro.Worker;
using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Clients;
using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Settings;
using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Connections;
using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Services;
using Refit;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        // Bind settings
        var rabbitSettings = context.Configuration.GetSection("RabbitMq").Get<RabbitMqSettings>();
        var apiSettings = context.Configuration.GetSection("Api").Get<ApiSettings>();

        // Injeção RabbitMQ
        services.AddSingleton(rabbitSettings!);
        services.AddSingleton<IRabbitMqConnection>(sp =>
            new RabbitMqConnection(rabbitSettings!.HostName, rabbitSettings.UserName, rabbitSettings.Password));

        services.AddSingleton<IRabbitMqMessageSenderService, RabbitMqMessageSenderService>();

        // Refit clients com BaseUrl do appsettings
        services.AddRefitClient<IEmailApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings!.BaseUrl));

        services.AddRefitClient<IProcessosApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings!.BaseUrl));

        // Worker
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
