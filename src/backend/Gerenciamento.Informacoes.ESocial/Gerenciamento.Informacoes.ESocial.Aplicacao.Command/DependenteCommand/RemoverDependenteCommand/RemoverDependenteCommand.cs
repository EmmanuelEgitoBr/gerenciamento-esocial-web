using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.RemoverDependenteCommand;

public class RemoverDependenteCommand : IRequest<ApiResponse<int>>
{
    public int DependenteId { get; set; }
}
