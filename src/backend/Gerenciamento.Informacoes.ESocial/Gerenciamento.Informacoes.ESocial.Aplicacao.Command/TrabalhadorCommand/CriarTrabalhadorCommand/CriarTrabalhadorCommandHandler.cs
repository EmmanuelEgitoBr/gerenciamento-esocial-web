using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.CriarTrabalhadorCommand;

public class CriarTrabalhadorCommandHandler : IRequestHandler<CriarTrabalhadorCommand, ApiResponse<int>>
{
    private readonly ITrabalhadorRepository _trabalhadorRepository;

    public CriarTrabalhadorCommandHandler(ITrabalhadorRepository trabalhadorRepository)
    {
        _trabalhadorRepository = trabalhadorRepository;
    }

    public async Task<ApiResponse<int>> Handle(CriarTrabalhadorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var trabalhador = CriarEntidadeTrabalhador(request);
            var result = await _trabalhadorRepository.AddAsync(trabalhador);

            return new ApiResponse<int>(true, result.TrabalhadorId, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }

    private Trabalhador CriarEntidadeTrabalhador(CriarTrabalhadorCommand request)
    {
        return new Trabalhador
        {
            Tipo = request.Tipo,
            Nome = request.Nome,
            SexoId = request.SexoId,
            RacaCorId = request.RacaCorId,
            EstadoCivilId = request.EstadoCivilId,
            GrauInstrucaoId = request.GrauInstrucaoId,
            IsPrimeiroEmprego = request.IsPrimeiroEmprego,
            CodigoNomeTravTrans = request.CodigoNomeTravTrans,
            DataNascimento = request.DataNascimento,
            MunicipioNascimento = request.MunicipioNascimento,
            UfNascimento = request.UfNascimento,
            PaisNascimento = request.PaisNascimento,
            Nacionalidade = request.Nacionalidade,
            NomeMae = request.NomeMae,
            NomePai = request.NomePai,
            DocumentosPessoais = request.DocumentosPessoais,
            EnderecoResidencial = request.EnderecoResidencial,
            DadosDeficiencia = request.DadosDeficiencia,
            RecebeBeneficioPrevidencia = request.RecebeBeneficioPrevidencia,
            Contato = request.Contato,
            IsVerificado = request.IsVerificado,
            DataCadastro = request.DataCadastro,
            DataUltimaAtualizacao = request.DataUltimaAtualizacao
        };
    }
}
