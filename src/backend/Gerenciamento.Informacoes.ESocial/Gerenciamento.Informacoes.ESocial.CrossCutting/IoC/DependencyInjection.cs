using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.CriarTrabalhadorCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Mappings;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Mappings;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByIdQuery;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Base;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddAutoMapper(typeof(MappingQueryConfiguration));
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

    public static void AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CriarTrabalhadorCommand).Assembly));
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(GetTrabalhadorByIdQuery).Assembly));
    }
}
