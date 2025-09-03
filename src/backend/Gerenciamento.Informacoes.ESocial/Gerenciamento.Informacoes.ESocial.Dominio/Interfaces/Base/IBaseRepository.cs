namespace Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Base;

public interface IBaseRepository<T>
{
    Task<T> AddAsync(T obj);
    Task<T> GetByIdAsync(int? id);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T obj);
    Task RemoveAsync(T obj);
}
