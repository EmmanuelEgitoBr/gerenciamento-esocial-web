using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Enums;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.AtualizarTrabalhadorCommand;

public class AtualizarTrabalhadorCommandHandler : IRequestHandler<AtualizarTrabalhadorCommand, ApiResponse<int>>
{
    private readonly ITrabalhadorRepository _trabalhadorRepository;

    public AtualizarTrabalhadorCommandHandler(ITrabalhadorRepository trabalhadorRepository)
    {
        _trabalhadorRepository = trabalhadorRepository;
    }

    public async Task<ApiResponse<int>> Handle(AtualizarTrabalhadorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var trabalhador = CriarEntidadeTrabalhador(request);
            await _trabalhadorRepository.UpdateAsync(trabalhador);

            return new ApiResponse<int>(true, 0, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }

    private Trabalhador CriarEntidadeTrabalhador(AtualizarTrabalhadorCommand request)
    {
        return new Trabalhador
        {
            Tipo = (TipoVinculo)request.Tipo,
            Nome = request.Nome,
            Sexo = (Sexo)request.Sexo,
            RacaCor = (RacaCor)request.RacaCor,
            EstadoCivil = (EstadoCivil)request.EstadoCivil,
            GrauInstrucao = (GrauInstrucao)request.GrauInstrucao,
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
