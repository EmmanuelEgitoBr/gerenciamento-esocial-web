using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.RemoverCedidoCommand;

public class RemoverCedidoCommandHandler : IRequestHandler<RemoverCedidoCommand, ApiResponse<int>>
{
    private readonly ICedidoRepository _cedidoRepository;

    public RemoverCedidoCommandHandler(ICedidoRepository cedidoRepository)
    {
        _cedidoRepository = cedidoRepository;
    }

    public async Task<ApiResponse<int>> Handle(RemoverCedidoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cedido = await _cedidoRepository.GetByIdAsync(request.CedidoId);

            if(cedido == null) return new ApiResponse<int>(false, 0, "Cedido(a) não encontrado(a)");

            await _cedidoRepository.RemoveAsync(cedido);

            return new ApiResponse<int>(true, cedido.CedidoId, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }
}
