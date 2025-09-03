using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.CriarCedidoCommand;

public class CriarCedidoCommandHandler : IRequestHandler<CriarCedidoCommand, ApiResponse<int>>
{
    private readonly ICedidoRepository _cedidoRepository;

    public CriarCedidoCommandHandler(ICedidoRepository cedidoRepository)
    {
        _cedidoRepository = cedidoRepository;
    }

    public async Task<ApiResponse<int>> Handle(CriarCedidoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cedido = CriarEntidadeCedido(request);
            var result = await _cedidoRepository.AddAsync(cedido);

            return new ApiResponse<int>(true, result.CedidoId, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }

    private Cedido CriarEntidadeCedido(CriarCedidoCommand request)
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
