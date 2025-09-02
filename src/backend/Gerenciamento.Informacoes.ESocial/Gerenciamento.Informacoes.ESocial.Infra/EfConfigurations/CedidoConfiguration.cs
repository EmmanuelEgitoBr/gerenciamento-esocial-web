using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.EfConfigurations;

public class CedidoConfiguration : IEntityTypeConfiguration<Cedido>
{
    public void Configure(EntityTypeBuilder<Cedido> builder)
    {
        builder.HasKey(c => c.CedidoId);

        builder.HasOne(c => c.Trabalhador)
               .WithMany(t => t.Cedidos)
               .HasForeignKey(c => c.TrabalhadorId);
    }
}

