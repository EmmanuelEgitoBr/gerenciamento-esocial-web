using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Cadastro;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;

public class LogStatusCadastroRepository(AppDbContext db) : BaseRepository<LogStatusCadastro>(db), ILogStatusCadastroRepository
{
}
