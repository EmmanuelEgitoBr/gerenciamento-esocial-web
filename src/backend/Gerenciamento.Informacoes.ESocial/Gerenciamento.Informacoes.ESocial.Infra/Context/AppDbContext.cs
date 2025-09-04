using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Cadastro;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Arquivo> Arquivos { get; set; }
    public DbSet<Cedido> Cedidos { get; set; }
    public DbSet<Dependente> Dependentes { get; set; }
    public DbSet<Estagiario> Estagiarios { get; set; }
    public DbSet<Trabalhador> Trabalhadores { get; set; }
    public DbSet<LogStatusCadastro> LogStatusCadastros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
