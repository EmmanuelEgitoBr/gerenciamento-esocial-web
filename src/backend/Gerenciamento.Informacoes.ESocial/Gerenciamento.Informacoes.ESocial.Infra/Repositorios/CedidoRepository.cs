﻿using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;

public class CedidoRepository(AppDbContext db) : BaseRepository<Cedido>(db), ICedidoRepository
{
    public async Task<Cedido> GetCedidoByTrabalhadorIdAsync(int trabalhadorId)
    {
        return await db.Cedidos
            .AsNoTracking()
            .FirstAsync(p => p.TrabalhadorId == trabalhadorId);
    }
}
