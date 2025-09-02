﻿namespace Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Base;

public interface IRepositoryBase<T>
{
    Task<T> AddAsync(T obj);
    Task<T> GetByIdAsync(int? id);
    IEnumerable<T> GetAllAsync();
    Task UpdateAsync(T obj);
    Task RemoveAsync(T obj);
}
