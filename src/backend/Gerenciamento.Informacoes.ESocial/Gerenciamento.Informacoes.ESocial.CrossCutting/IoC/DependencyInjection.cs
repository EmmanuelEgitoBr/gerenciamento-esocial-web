using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Mappings;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Base;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
        IMapper mapper = MappingConfiguration.RegisterMap().CreateMapper();
        services.AddScoped<IMapper>(_ => mapper);
        services.AddAutoMapper(typeof(MappingConfiguration));
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IArquivoRepository, ArquivoRepository>();
        services.AddScoped<ICedidoRepository, CedidoRepository>();
        services.AddScoped<IDependenteRepository, DependenteRepository>();
        services.AddScoped<IEstagiarioRepository, EstagiarioRepository>();
        services.AddScoped<ITrabalhadorRepository, TrabalhadorRepository>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }

    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "API de Gerenciamento de Informações",
                Version = "v1",
                Description = "Api destinada para o gerenciamento de informações que estão integradas ao E-Social",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact() 
                { 
                    Name = "Emmanuel Egito",
                    Url = new Uri("https://github.com/EmmanuelEgitoBr/gerenciamento-esocial-web")
                }
            });

            // Adiciona o arquivo XML gerado para incluir os comentários
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
