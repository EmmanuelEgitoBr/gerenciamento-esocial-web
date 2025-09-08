using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByUserIdQuery;

public class GetTrabalhadorByUserIdQueryHandler : IRequestHandler<GetTrabalhadorByUserIdQuery, ApiResponse<TrabalhadorDto>>
{
    private readonly ITrabalhadorRepository _trabalhadorRepository;

    public GetTrabalhadorByUserIdQueryHandler(ITrabalhadorRepository trabalhadorRepository)
    {
        _trabalhadorRepository = trabalhadorRepository;
    }

    public async Task<ApiResponse<TrabalhadorDto>> Handle(GetTrabalhadorByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _trabalhadorRepository.GetTrabalhadorByUserIdAsync(request.UserId);

            if (entity == null) return new ApiResponse<TrabalhadorDto>(false, null, "Trabalhador(a) não encontrado(a)");

            var dto = CriarTrabalhadorDto(entity);
            return new ApiResponse<TrabalhadorDto>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<TrabalhadorDto>(false, null, ex.Message);
        }
    }

    private TrabalhadorDto CriarTrabalhadorDto(Trabalhador trabalhador)
    {
        return new TrabalhadorDto
        {
            TrabalhadorId = trabalhador.TrabalhadorId,
            UserId = trabalhador.UserId,
            Tipo = trabalhador.Tipo,
            Nome = trabalhador.Nome,
            Sexo = trabalhador.Sexo,
            RacaCor = trabalhador.RacaCor,
            EstadoCivil = trabalhador.EstadoCivil,
            GrauInstrucao = trabalhador.GrauInstrucao,
            IsPrimeiroEmprego = trabalhador.IsPrimeiroEmprego,
            CodigoNomeTravTrans = trabalhador.CodigoNomeTravTrans,
            DataNascimento = trabalhador.DataNascimento,
            MunicipioNascimento = trabalhador.MunicipioNascimento,
            UfNascimento = trabalhador.UfNascimento,
            PaisNascimento = trabalhador.PaisNascimento,
            Nacionalidade = trabalhador.Nacionalidade,
            NomeMae = trabalhador.NomeMae,
            NomePai = trabalhador.NomePai,
            DocumentosPessoais = trabalhador.DocumentosPessoais,
            EnderecoResidencial = trabalhador.EnderecoResidencial,
            DadosDeficiencia = trabalhador.DadosDeficiencia,
            RecebeBeneficioPrevidencia = trabalhador.RecebeBeneficioPrevidencia,
            Contato = trabalhador.Contato,
            StatusCadastro = trabalhador.StatusCadastro,
            DataCadastro = trabalhador.DataCadastro,
            DataUltimaAtualizacao = trabalhador.DataUltimaAtualizacao
        };
    }
}
