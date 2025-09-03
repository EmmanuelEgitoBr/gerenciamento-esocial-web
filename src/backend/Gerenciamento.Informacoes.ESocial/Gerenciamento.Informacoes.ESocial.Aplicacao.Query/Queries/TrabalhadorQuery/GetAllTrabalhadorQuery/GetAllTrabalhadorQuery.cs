using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetAllTrabalhadorQuery;

public class GetAllTrabalhadorQuery : IRequest<ApiResponse<IEnumerable<TrabalhadorDto>>>
{
}
