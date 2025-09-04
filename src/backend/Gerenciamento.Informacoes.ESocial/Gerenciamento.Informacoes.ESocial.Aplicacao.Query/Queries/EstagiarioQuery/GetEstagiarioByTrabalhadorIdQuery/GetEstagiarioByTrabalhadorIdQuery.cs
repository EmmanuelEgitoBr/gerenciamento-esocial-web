using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetEstagiarioByTrabalhadorIdQuery;

public class GetEstagiarioByTrabalhadorIdQuery : IRequest<ApiResponse<EstagiarioDto>>
{
    public int TrabalhadorId { get; set; }
}
