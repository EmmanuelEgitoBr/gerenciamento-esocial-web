using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Context;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios.Base;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;

public class ArquivoRepository(AppDbContext db) : BaseRepository<Arquivo>(db), IArquivoRepository
{
}
