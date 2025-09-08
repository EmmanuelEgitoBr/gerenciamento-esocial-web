using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByUserIdQuery;

public class GetTrabalhadorByUserIdQuery : IRequest<ApiResponse<TrabalhadorDto>>
{
    public string UserId { get; set; }
    public GetTrabalhadorByUserIdQuery(string userId)
    {
        UserId = userId;
    }
}
