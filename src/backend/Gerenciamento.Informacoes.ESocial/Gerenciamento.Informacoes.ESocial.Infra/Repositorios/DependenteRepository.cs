using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;

public class DependenteRepository(AppDbContext db) : BaseRepository<Dependente>(db), IDependenteRepository
{
}
