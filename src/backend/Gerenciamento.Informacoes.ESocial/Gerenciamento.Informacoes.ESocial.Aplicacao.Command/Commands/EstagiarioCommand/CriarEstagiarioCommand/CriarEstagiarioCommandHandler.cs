using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.CriarEstagiarioCommand;

public class CriarEstagiarioCommandHandler : IRequestHandler<CriarEstagiarioCommand, ApiResponse<int>>
{
    private readonly IEstagiarioRepository _estagiarioRepository;

    public CriarEstagiarioCommandHandler(IEstagiarioRepository estagiarioRepository)
    {
        _estagiarioRepository = estagiarioRepository;
    }

    public async Task<ApiResponse<int>> Handle(CriarEstagiarioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var estagiario = CriarEntidadeEstagiario(request);
            var result = await _estagiarioRepository.AddAsync(estagiario);

            return new ApiResponse<int>(true, result.EstagiarioId, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }

    private Estagiario CriarEntidadeEstagiario(CriarEstagiarioCommand request)
    {
        return new Estagiario
        {
            TrabalhadorId = request.TrabalhadorId,
            NaturezaEstagio = request.NaturezaEstagio,
            AreaAtuacaoId = request.AreaAtuacaoId,
            RazaoSocialInstEnsino = request.RazaoSocialInstEnsino,
            CnpjInstEnsino = request.CnpjInstEnsino,
            NomeSupervisor = request.NomeSupervisor,
            EnderecoInstEnsino = request.EnderecoInstEnsino
        };
    }
}
