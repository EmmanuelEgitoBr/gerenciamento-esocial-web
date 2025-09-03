using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.RemoverEstagiarioCommand;

public class RemoverEstagiarioCommandHandler : IRequestHandler<RemoverEstagiarioCommand, ApiResponse<int>>
{
    private readonly IEstagiarioRepository _estagiarioRepository;

    public RemoverEstagiarioCommandHandler(IEstagiarioRepository estagiarioRepository)
    {
        _estagiarioRepository = estagiarioRepository;
    }

    public async Task<ApiResponse<int>> Handle(RemoverEstagiarioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var estagiario = await _estagiarioRepository.GetByIdAsync(request.EstagiarioId);

            if (estagiario == null) return new ApiResponse<int>(false, 0, "Estagiário(a) não encontrado(a)");

            await _estagiarioRepository.RemoveAsync(estagiario);

            return new ApiResponse<int>(true, estagiario.EstagiarioId, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }
}
