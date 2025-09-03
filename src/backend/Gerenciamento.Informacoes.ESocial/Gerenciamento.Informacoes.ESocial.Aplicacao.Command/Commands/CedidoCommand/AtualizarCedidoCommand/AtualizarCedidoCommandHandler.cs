using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.AtualizarCedidoCommand;

public class AtualizarCedidoCommandHandler : IRequestHandler<AtualizarCedidoCommand, ApiResponse<int>>
{
    private readonly ICedidoRepository _cedidoRepository;

    public AtualizarCedidoCommandHandler(ICedidoRepository cedidoRepository)
    {
        _cedidoRepository = cedidoRepository;
    }

    public async Task<ApiResponse<int>> Handle(AtualizarCedidoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cedido = CriarEntidadeCedido(request);
            await _cedidoRepository.UpdateAsync(cedido);

            return new ApiResponse<int>(true, 0, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }

    private Cedido CriarEntidadeCedido(AtualizarCedidoCommand request)
    {
        return new Cedido()
        {
            TrabalhadorId = request.TrabalhadorId,
            CnpjEmpregadoCedido = request.CnpjEmpregadoCedido,
            MatriculaTrabalhador = request.MatriculaTrabalhador,
            DataAdmissao = request.DataAdmissao,
            TipoRegTrab = request.TipoRegTrab,
            TipoRegPrev = request.TipoRegPrev,
            OnusCessReqId = request.OnusCessReqId,
            CategoriaId = request.CategoriaId
        };
    }
}