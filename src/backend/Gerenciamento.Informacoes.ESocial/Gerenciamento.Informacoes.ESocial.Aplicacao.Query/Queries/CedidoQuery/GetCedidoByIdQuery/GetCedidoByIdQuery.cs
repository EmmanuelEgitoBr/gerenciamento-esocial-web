using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetCedidoByIdQuery;

public class GetCedidoByIdQuery : IRequest<ApiResponse<CedidoDto>>
{
    public int CedidoId { get; set; }
}
