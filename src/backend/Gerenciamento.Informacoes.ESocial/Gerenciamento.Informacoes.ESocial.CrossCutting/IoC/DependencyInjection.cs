using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
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
}
