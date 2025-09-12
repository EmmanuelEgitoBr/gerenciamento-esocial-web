using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.CriarTrabalhadorCommand;

public class CriarTrabalhadorCommand : IRequest<ApiResponse<int>>
{
    public string? UserId { get; set; }
    public int Tipo { get; set; }
    public string? Nome { get; set; }
    public int Sexo { get; set; }
    public int RacaCor { get; set; }
    public int EstadoCivil { get; set; }
    public int GrauInstrucao { get; set; }
    public bool IsPrimeiroEmprego { get; set; }
    public string? CodigoNomeTravTrans { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? MunicipioNascimento { get; set; }
    public string? UfNascimento { get; set; }
    public string? PaisNascimento { get; set; }
    public string? Nacionalidade { get; set; }
    public string? NomeMae { get; set; }
    public string? NomePai { get; set; }
    public Documento? DocumentosPessoais { get; set; }
    public Endereco? EnderecoResidencial { get; set; }
    public Deficiencia? DadosDeficiencia { get; set; }
    public bool RecebeBeneficioPrevidencia { get; set; }
    public Contato? Contato { get; set; }
    public int StatusCadastro { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    public DateTime? DataUltimaAtualizacao { get; set; }
}
