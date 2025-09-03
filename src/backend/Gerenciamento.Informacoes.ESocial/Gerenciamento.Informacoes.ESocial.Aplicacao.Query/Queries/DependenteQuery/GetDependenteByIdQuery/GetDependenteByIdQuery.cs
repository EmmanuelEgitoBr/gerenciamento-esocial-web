using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependenteByIdQuery;

public class GetDependenteByIdQuery : IRequest<ApiResponse<DependenteDto>>
{
    public int DependenteId { get; set; }
}
