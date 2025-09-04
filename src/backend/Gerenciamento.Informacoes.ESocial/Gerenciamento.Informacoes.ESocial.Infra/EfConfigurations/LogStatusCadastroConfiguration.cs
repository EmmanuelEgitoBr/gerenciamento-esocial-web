using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Cadastro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.EfConfigurations;

public class LogStatusCadastroConfiguration : IEntityTypeConfiguration<LogStatusCadastro>
{
    public void Configure(EntityTypeBuilder<LogStatusCadastro> builder)
    {
        builder.HasKey(l => l.LogStatusCadastroId);
    }
}
