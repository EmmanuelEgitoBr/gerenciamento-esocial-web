using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetAllDependenteQuery;

public class GetAllDependenteQuery : IRequest<ApiResponse<IEnumerable<DependenteDto>>>
{
}
