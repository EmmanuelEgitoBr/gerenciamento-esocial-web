using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependenteByTrabalhadorIdQuery;

public class GetDependenteByTrabalhadorIdQuery : IRequest<ApiResponse<IEnumerable<DependenteDto>>>
{
    public int TrabalhadorId { get; set; }
}
