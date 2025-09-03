using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetAllCedidosQuery;

public class GetAllCedidosQuery : IRequest<ApiResponse<IEnumerable<CedidoDto>>>
{
}
