using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.EfConfigurations;

public class TrabalhadorConfiguration : IEntityTypeConfiguration<Trabalhador>
{
    public void Configure(EntityTypeBuilder<Trabalhador> builder)
    {
        builder.HasKey(t => t.TrabalhadorId);

        builder.HasOne(t => t.Cedido)
               .WithOne(c => c.Trabalhador)
               .HasForeignKey<Cedido>(c => c.TrabalhadorId);

        // Relacionamento 1:N
        builder.HasMany(t => t.Dependentes)
               .WithOne(d => d.Trabalhador)
               .HasForeignKey(d => d.TrabalhadorId);

        builder.HasOne(t => t.Estagiario)
               .WithOne(e => e.Trabalhador)
               .HasForeignKey<Estagiario>(e => e.TrabalhadorId);

        // Value Objects (colunas embutidas)
        builder.OwnsOne(t => t.DocumentosPessoais);
        builder.OwnsOne(t => t.EnderecoResidencial);
        builder.OwnsOne(t => t.DadosDeficiencia);
        builder.OwnsOne(t => t.Contato);
    }
}
