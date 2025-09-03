using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByIdQuery;

public class GetTrabalhadorByIdQuery : IRequest<ApiResponse<TrabalhadorDto>>
{
    public int TrabalhadorId { get; set; }
}
