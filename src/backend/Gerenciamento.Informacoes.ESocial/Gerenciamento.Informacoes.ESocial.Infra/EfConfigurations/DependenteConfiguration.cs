using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.EfConfigurations;

public class DependenteConfiguration : IEntityTypeConfiguration<Dependente>
{
    public void Configure(EntityTypeBuilder<Dependente> builder)
    {
        builder.HasKey(d => d.DependenteId);

        builder.HasOne(d => d.Trabalhador)
               .WithMany(t => t.Dependentes)
               .HasForeignKey(d => d.TrabalhadorId);
    }
}