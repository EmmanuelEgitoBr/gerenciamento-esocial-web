using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;

public class EstagiarioRepository(AppDbContext db) : BaseRepository<Estagiario>(db), IEstagiarioRepository
{
    public async Task<Estagiario> GetEstagiarioByTrabalhadorIdAsync(int trabalhadorId)
    {
        return await db.Estagiarios
            .AsNoTracking()
            .FirstAsync(p => p.TrabalhadorId == trabalhadorId);
    }
}
