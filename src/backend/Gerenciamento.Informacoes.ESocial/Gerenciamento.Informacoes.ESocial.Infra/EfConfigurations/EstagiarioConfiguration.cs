using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.EfConfigurations;

public class EstagiarioConfiguration : IEntityTypeConfiguration<Estagiario>
{
    public void Configure(EntityTypeBuilder<Estagiario> builder)
    {
        builder.HasKey(e => e.EstagiarioId);

        builder.HasOne(e => e.Trabalhador)
               .WithMany(t => t.Estagiarios)
               .HasForeignKey(e => e.TrabalhadorId);

        // Endereço da instituição como Value Object
        builder.OwnsOne(e => e.EnderecoInstEnsino);
    }
}

