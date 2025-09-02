using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.EfConfigurations;

public class ArquivoConfiguration : IEntityTypeConfiguration<Arquivo>
{
    public void Configure(EntityTypeBuilder<Arquivo> builder)
    {
        builder.HasKey(a => a.ArquivoId);

        builder.Property(a => a.Tipo)
               .HasMaxLength(50);

        builder.Property(a => a.NomeArquivo)
               .HasMaxLength(255);

        // Relacionamento opcional com Trabalhador
        builder.HasOne<Trabalhador>()
               .WithMany() // se quiser que Trabalhador tenha ICollection<Arquivo>, substituímos por .WithMany(t => t.Arquivos)
               .HasForeignKey(a => a.TrabalhadorId)
               .OnDelete(DeleteBehavior.SetNull); // se deletar Trabalhador, o Arquivo fica "solto"
    }
}
