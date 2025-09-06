using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Mappings;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.CriarTrabalhadorCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByIdQuery;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Settings;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Base;
using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Connections;
using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Messaging.Services;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gerenciamento.Informacoes.ESocial.CrossCutting.IoC;

public static class DependencyInjection
{
    public static void AddSqlServerConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            options.EnableSensitiveDataLogging();
        });
    }

    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        IMapper mapper = Aplicacao.Query.Mappings.MappingConfiguration.RegisterMap().CreateMapper();
        services.AddScoped<IMapper>(_ => mapper);
        services.AddAutoMapper(typeof(Aplicacao.Query.Mappings.MappingConfiguration));

        IMapper autoMapper = MapConfiguration.RegisterMap().CreateMapper();
        services.AddScoped<IMapper>(_ => autoMapper);
        services.AddAutoMapper(typeof(MapConfiguration));
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IArquivoRepository, ArquivoRepository>();
        services.AddScoped<ICedidoRepository, CedidoRepository>();
        services.AddScoped<IDependenteRepository, DependenteRepository>();
        services.AddScoped<IEstagiarioRepository, EstagiarioRepository>();
        services.AddScoped<ITrabalhadorRepository, TrabalhadorRepository>();
        services.AddScoped<ILogStatusCadastroRepository, LogStatusCadastroRepository>();
        services.AddScoped<ILogEnvioEmailRepository, LogEnvioEmailRepository>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IArquivoService, ArquivoService>();
        services.AddScoped<ITrabalhadorService, TrabalhadorService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CriarTrabalhadorCommand).Assembly));
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(GetTrabalhadorByIdQuery).Assembly));
    }

    public static void AddSecurityInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
    {
        string secretKey = configuration["JWT:SecretKey"]
                ?? throw new ArgumentException("Invalid Secret Key");
        string validIssuer = configuration["JWT:ValidIssuer"]!;
        string validAudience = configuration["JWT:ValidAudience"]!;

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            options.AddPolicy("SuperAdminOnly",
                policy => policy.RequireRole("Admin").RequireClaim("id", "eegito"));
            options.AddPolicy("ExclusiveOnly",
                                policy => policy.RequireAssertion(context =>
                                    context.User.HasClaim(claim => claim.Type == "id" &&
                                                            claim.Value == "eegito" ||
                                                            context.User.IsInRole("SuperAdmin"))));
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = validAudience,
                ValidIssuer = validIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://www.apirequest.io");
            });
        });
    }

    public static void AddRabbitMqConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitConfig = configuration.GetSection("RabbitMq");
        services.AddSingleton<IRabbitMqConnection>(sp =>
                new RabbitMqConnection(
                    rabbitConfig["HostName"]!,
                    rabbitConfig["UserName"]!,
                    rabbitConfig["Password"]!
                ));
        services.AddScoped<IRabbitMqMessageSenderService, RabbitMqMessageSenderService>();
    }

    public static void AddEmailConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<EmailSettings>(
            builder.Configuration.GetSection("Smtp"));
        builder.Services.AddScoped<IEmailService, EmailService>();
    }
}
