using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.RemoverDependenteCommand;

public class RemoverDependenteCommandHandler : IRequestHandler<RemoverDependenteCommand, ApiResponse<int>>
{
    private readonly IDependenteRepository _dependenteRepository;

    public RemoverDependenteCommandHandler(IDependenteRepository dependenteRepository)
    {
        _dependenteRepository = dependenteRepository;
    }

    public async Task<ApiResponse<int>> Handle(RemoverDependenteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var dependente = await _dependenteRepository.GetByIdAsync(request.DependenteId);

            if (dependente == null) return new ApiResponse<int>(false, 0, "Dependente não encontrado(a)");

            await _dependenteRepository.RemoveAsync(dependente);

            return new ApiResponse<int>(true, dependente.DependenteId, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }
}
