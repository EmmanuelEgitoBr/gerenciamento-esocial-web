using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetAllEstagiarioQuery;

public class GetAllEstagiarioQuery : IRequest<ApiResponse<IEnumerable<EstagiarioDto>>>
{
}
