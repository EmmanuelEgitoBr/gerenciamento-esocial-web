using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Arquivo> Arquivos { get; set; }
    public DbSet<Cedido> Cedidos { get; set; }
    public DbSet<Dependente> Dependentes { get; set; }
    public DbSet<Estagiario> Estagiarios { get; set; }
    public DbSet<Trabalhador> Trabalhadores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
