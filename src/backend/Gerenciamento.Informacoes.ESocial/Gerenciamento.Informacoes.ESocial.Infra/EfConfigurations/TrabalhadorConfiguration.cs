using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.EfConfigurations;

public class TrabalhadorConfiguration : IEntityTypeConfiguration<Trabalhador>
{
    public void Configure(EntityTypeBuilder<Trabalhador> builder)
    {
        builder.HasKey(t => t.TrabalhadorId);

        // Relacionamentos 1:N
        builder.HasMany(t => t.Cedidos)
               .WithOne(c => c.Trabalhador)
               .HasForeignKey(c => c.TrabalhadorId);

        builder.HasMany(t => t.Dependentes)
               .WithOne(d => d.Trabalhador)
               .HasForeignKey(d => d.TrabalhadorId);

        builder.HasMany(t => t.Estagiarios)
               .WithOne(e => e.Trabalhador)
               .HasForeignKey(e => e.TrabalhadorId);

        // Value Objects (colunas embutidas)
        builder.OwnsOne(t => t.DocumentosPessoais);
        builder.OwnsOne(t => t.EnderecoResidencial);
        builder.OwnsOne(t => t.DadosDeficiencia);
        builder.OwnsOne(t => t.Contato);
    }
}
