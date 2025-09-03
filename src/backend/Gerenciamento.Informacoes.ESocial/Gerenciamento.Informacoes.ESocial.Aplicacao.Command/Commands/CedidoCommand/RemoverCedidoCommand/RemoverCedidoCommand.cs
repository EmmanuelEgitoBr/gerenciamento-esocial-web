using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.RemoverCedidoCommand;

public class RemoverCedidoCommand : IRequest<ApiResponse<int>>
{
    public int CedidoId { get; set; }
}
