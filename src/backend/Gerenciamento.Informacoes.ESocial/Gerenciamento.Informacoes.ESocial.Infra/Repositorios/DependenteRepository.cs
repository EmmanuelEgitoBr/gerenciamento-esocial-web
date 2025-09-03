using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;

public class DependenteRepository(AppDbContext db) : BaseRepository<Dependente>(db), IDependenteRepository
{
    public async Task<IEnumerable<Dependente>> GetDependentesByTrabalhadorIdAsync(int trabalhadorId)
    {
        return await db.Dependentes
            .AsNoTracking()
            .Where(p => p.TrabalhadorId == trabalhadorId)
            .ToListAsync();
    }
}
