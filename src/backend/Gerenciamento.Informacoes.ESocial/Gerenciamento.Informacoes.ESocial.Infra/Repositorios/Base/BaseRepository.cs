using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Base;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _db;

    public BaseRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<T> AddAsync(T obj)
    {
        await _db.Set<T>().AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<T> GetByIdAsync(int? id)
    {
        if (id == null) return null!;
        return await _db.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _db.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task UpdateAsync(T obj)
    {
        _db.Set<T>().Update(obj);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(T obj)
    {
        _db.Set<T>().Remove(obj);
        await _db.SaveChangesAsync();
    }
}
