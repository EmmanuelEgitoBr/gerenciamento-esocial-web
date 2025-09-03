using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetCedidoByTrabalhadorIdQuery;

public class GetCedidoByTrabalhadorIdQuery : IRequest<ApiResponse<IEnumerable<CedidoDto>>>
{
    public int TrabalhadorId { get; set; }
}
