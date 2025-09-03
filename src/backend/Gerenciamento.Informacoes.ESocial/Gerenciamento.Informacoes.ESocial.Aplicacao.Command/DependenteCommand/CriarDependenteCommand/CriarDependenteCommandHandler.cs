using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.CriarDependenteCommand;

public class CriarDependenteCommandHandler : IRequestHandler<CriarDependenteCommand, ApiResponse<int>>
{
    private readonly IDependenteRepository _dependenteRepository;

    public CriarDependenteCommandHandler(IDependenteRepository dependenteRepository)
    {
        _dependenteRepository = dependenteRepository;
    }

    public async Task<ApiResponse<int>> Handle(CriarDependenteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var dependente = CriarEntidadeDependente(request);
            var result = await _dependenteRepository.AddAsync(dependente);

            return new ApiResponse<int>(true, result.DependenteId, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }

    private Dependente CriarEntidadeDependente(CriarDependenteCommand request)
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
