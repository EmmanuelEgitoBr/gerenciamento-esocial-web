using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.AtualizarDependenteCommand;

public class AtualizarDependenteCommandHandler : IRequestHandler<AtualizarDependenteCommand, ApiResponse<int>>
{
    private readonly IDependenteRepository _dependenteRepository;

    public AtualizarDependenteCommandHandler(IDependenteRepository dependenteRepository)
    {
        _dependenteRepository = dependenteRepository;
    }

    public async Task<ApiResponse<int>> Handle(AtualizarDependenteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var dependente = CriarEntidadeDependente(request);
            await _dependenteRepository.UpdateAsync(dependente);

            return new ApiResponse<int>(true, 0, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }

    private Dependente CriarEntidadeDependente(AtualizarDependenteCommand request)
    {
        return new Dependente
        {
            TrabalhadorId = request.TrabalhadorId,
            TipoDependente = request.TipoDependente,
            NomeDependente = request.NomeDependente,
            DataNascimento = request.DataNascimento,
            CpfDependente = request.CpfDependente,
            EhDepTrabalhadorIrrf = request.EhDepTrabalhadorIrrf,
            EhDepIncapaFisMentTrab = request.EhDepIncapaFisMentTrab,
            EhDependentePensao = request.EhDependentePensao,
            TelefoneResponsavel = request.TelefoneResponsavel
        };
    }
}
