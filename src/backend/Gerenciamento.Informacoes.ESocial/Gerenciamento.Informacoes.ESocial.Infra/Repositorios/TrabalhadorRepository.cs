using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;

public class TrabalhadorRepository(AppDbContext db) : BaseRepository<Trabalhador>(db), ITrabalhadorRepository
{
    public async Task<Trabalhador?> GetTrabalhadorByUserIdAsync(string userId)
    {
        return await db.Trabalhadores
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .FirstAsync();
    }
}
