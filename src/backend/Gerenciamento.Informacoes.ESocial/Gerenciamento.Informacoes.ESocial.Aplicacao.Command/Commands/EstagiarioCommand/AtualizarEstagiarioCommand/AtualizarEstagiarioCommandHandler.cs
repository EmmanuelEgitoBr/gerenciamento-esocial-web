using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.AtualizarEstagiarioCommand;

public class AtualizarEstagiarioCommandHandler : IRequestHandler<AtualizarEstagiarioCommand, ApiResponse<int>>
{
    private readonly IEstagiarioRepository _estagiarioRepository;

    public AtualizarEstagiarioCommandHandler(IEstagiarioRepository estagiarioRepository)
    {
        _estagiarioRepository = estagiarioRepository;
    }

    public async Task<ApiResponse<int>> Handle(AtualizarEstagiarioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var estagiario = CriarEntidadeEstagiario(request);
            await _estagiarioRepository.UpdateAsync(estagiario);

            return new ApiResponse<int>(true, 0, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }

    private Estagiario CriarEntidadeEstagiario(AtualizarEstagiarioCommand request)
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
