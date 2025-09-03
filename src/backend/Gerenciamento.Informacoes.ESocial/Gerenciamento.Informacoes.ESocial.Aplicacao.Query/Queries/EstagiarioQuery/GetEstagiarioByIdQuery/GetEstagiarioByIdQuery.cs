using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetEstagiarioByIdQuery;

public class GetEstagiarioByIdQuery : IRequest<ApiResponse<EstagiarioDto>>
{
    public int EstagiarioId { get; set; }
}
