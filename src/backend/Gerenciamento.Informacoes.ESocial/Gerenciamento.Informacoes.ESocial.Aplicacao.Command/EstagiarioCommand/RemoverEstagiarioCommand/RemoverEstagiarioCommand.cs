using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.RemoverEstagiarioCommand;

public class RemoverEstagiarioCommand : IRequest<ApiResponse<int>>
{
    public int EstagiarioId { get; set; }
}
