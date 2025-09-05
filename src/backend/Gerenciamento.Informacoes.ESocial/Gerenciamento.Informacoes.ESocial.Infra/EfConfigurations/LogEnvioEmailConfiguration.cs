using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Cadastro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.EfConfigurations;

public class LogEnvioEmailConfiguration : IEntityTypeConfiguration<LogEnvioEmail>
{
    public void Configure(EntityTypeBuilder<LogEnvioEmail> builder)
    {
        builder.HasKey(l => l.LogEnvioEmailId);
    }
}
